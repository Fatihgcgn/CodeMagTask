using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Helpers;

namespace WebApi.Services;

public class SerialService
{
    private readonly CodeMagDbContext _db;

    public SerialService(CodeMagDbContext db)
    {
        _db = db;
    }

    public async Task<List<Serial>> GenerateAsync(Guid workOrderId)
    {
        var wo = await _db.WorkOrders
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == workOrderId);

        if (wo == null)
            throw new KeyNotFoundException("WorkOrder not found.");

        if (wo.Quantity <= 0)
            throw new InvalidOperationException("Quantity must be > 0.");

        // aynı WO için ikinci kez üretme (idempotent istemiyoruz)
        var already = await _db.Serials.AnyAsync(x => x.WorkOrderId == workOrderId);
        if (already)
            throw new InvalidOperationException("Serials already generated for this work order.");

        await using var tx = await _db.Database.BeginTransactionAsync();

        var produced = new List<Serial>(wo.Quantity);
        var gtin = wo.Product.GTIN;
        long baseNo = wo.SerialStart;

        for (int i = 0; i < wo.Quantity; i++)
        {
            long desired = baseNo + i;

            for (int attempt = 0; attempt < 5; attempt++)
            {
                var serialNo = desired.ToString();
                var gs1 = Gs1Builder.Build(gtin, serialNo, wo.ExpiryDate, wo.BatchNo);

                var entity = new Serial
                {
                    Id = Guid.NewGuid(),
                    WorkOrderId = wo.Id,
                    GTIN = gtin,
                    SerialNo = serialNo,
                    BatchNo = wo.BatchNo,
                    ExpiryDate = wo.ExpiryDate,
                    Gs1String = gs1,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Serials.Add(entity);

                try
                {
                    await _db.SaveChangesAsync();
                    produced.Add(entity);
                    break;
                }
                catch (DbUpdateException ex) when (IsUniqueViolation(ex))
                {
                    _db.Entry(entity).State = EntityState.Detached;
                    desired += wo.Quantity;
                }

                if (attempt == 4)
                    throw new InvalidOperationException("Serial collision. Try different SerialStart.");
            }
        }

        wo.Status = WebApi.Enum.WorkOrderStatus.Serialized;

        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        return produced;
    }

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        return ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx
               && (sqlEx.Number == 2601 || sqlEx.Number == 2627);
    }
}
