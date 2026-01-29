using Application.Abstractions;
using Application.Helpers;
using Data.Db;
using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public sealed class SerialService
{
    private readonly CodeMagDbContext _db;
    private readonly INumberSequence _seq;
    private readonly Gs1Builder _gs1;

    public SerialService(CodeMagDbContext db, INumberSequence seq, Gs1Builder gs1)
    {
        _db = db;
        _seq = seq;
        _gs1 = gs1;
    }

    public async Task<List<Serial>> GenerateAsync(
        Guid workOrderId,
        int count = 1,
        CancellationToken ct = default)
    {
        if (count <= 0) count = 1;
        if (count > 5000) count = 5000;

        var wo = await _db.WorkOrders
            .Include(x => x.Product)
            .SingleAsync(x => x.Id == workOrderId, ct);

        var list = new List<Serial>(count);

        for (int i = 0; i < count; i++)
        {
            var next = await _seq.NextAsync($"SERIAL:WO:{workOrderId}", ct);
            var serialNo = next.ToString(); // istersen: next.ToString("D10")

            // NOT: senin Gs1Builder method adı farklıysa burayı uyarlayacağız
            var gs1 = _gs1.BuildSerialGs1(
                gtin: wo.Product.GTIN,
                serial: serialNo,
                batchNo: wo.BatchNo,
                expiryDate: wo.ExpiryDate
            );

            list.Add(new Serial
            {
                Id = Guid.NewGuid(),
                WorkOrderId = workOrderId,
                GTIN = wo.Product.GTIN,
                SerialNo = serialNo,
                BatchNo = wo.BatchNo,
                ExpiryDate = wo.ExpiryDate,
                Gs1String = gs1,
                CreatedAt = DateTime.UtcNow
            });
        }

        _db.Serials.AddRange(list);
        await _db.SaveChangesAsync(ct);

        return list;
    }
}
