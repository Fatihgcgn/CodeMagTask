using Application.Abstractions;
using Application.Helpers;
using Data.Db;
using Data.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Application.Exceptions.DomainExceptions;

namespace Data.Services;

public sealed class SerialService
{
    private readonly CodeMagDbContext _db;
    private readonly INumberSequence _seq;
    private readonly Gs1Builder _gs1;
    private readonly IPrinterService _printer; // ✅ eklendi

    public SerialService(
        CodeMagDbContext db,
        INumberSequence seq,
        Gs1Builder gs1,
        IPrinterService printer) // ✅ eklendi
    {
        _db = db;
        _seq = seq;
        _gs1 = gs1;
        _printer = printer;
    }

    public async Task<List<Serial>> GenerateAsync(Guid workOrderId, int count, CancellationToken ct = default)
    {
        if (count <= 0)
            throw new ValidationException("count must be > 0");

        await using var tx = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);

        // ✅ Product dahil
        var wo = await _db.WorkOrders
            .Include(x => x.Product)
            .SingleOrDefaultAsync(x => x.Id == workOrderId, ct);

        if (wo == null)
            throw new NotFoundException($"WorkOrder not found: {workOrderId}");

        if (wo.Product == null)
            throw new ConflictException("WorkOrder has no Product loaded/assigned.");

        var existing = await _db.Serials.CountAsync(s => s.WorkOrderId == workOrderId, ct);
        var remaining = wo.Quantity - existing;

        if (remaining <= 0)
            throw new ConflictException($"Serial limit reached. Quantity={wo.Quantity}, Existing={existing}");

        if (count > remaining)
            throw new ConflictException($"Cannot generate {count} serial(s). Remaining={remaining}, Quantity={wo.Quantity}, Existing={existing}");

        var list = new List<Serial>(count);

        for (int i = 0; i < count; i++)
        {
            var next = await _seq.NextAsync($"SERIAL:{wo.Id}", ct);
            var serialNo = next.ToString().PadLeft(12, '0');

            var gs1 = _gs1.BuildSerialGs1(
                gtin: wo.Product.GTIN,
                serial: serialNo,
                batchNo: wo.BatchNo,
                expiryDate: wo.ExpiryDate
            );

            list.Add(new Serial
            {
                Id = Guid.NewGuid(),
                WorkOrderId = wo.Id,
                GTIN = wo.Product.GTIN,
                SerialNo = serialNo,
                BatchNo = wo.BatchNo,
                ExpiryDate = wo.ExpiryDate,
                Gs1String = gs1,
                CreatedAt = DateTime.UtcNow
            });
        }

        try
        {
            _db.Serials.AddRange(list);
            await _db.SaveChangesAsync(ct);

            // ✅ PRINT SIMULATION (case entegrasyon maddesi)
            // DB’ye kaydettikten sonra print et → rollback etkisi olmasın
            foreach (var s in list)
                await _printer.PrintAsync(s.Gs1String!, ct);

            await tx.CommitAsync(ct);
            return list;
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            // Unique violation olursa print’e hiç gitmeden buraya düşer
            throw new ConflictException("Duplicate serial detected. Try again.");
        }
    }

    private static bool IsUniqueViolation(DbUpdateException ex)
        => ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627);


}