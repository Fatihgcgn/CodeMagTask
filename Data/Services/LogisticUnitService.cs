using Application.Abstractions;
using Application.Helpers;
using Data.Db;
using Data.Entity;
using Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public sealed class LogisticUnitService
{
    private readonly CodeMagDbContext _db;
    private readonly INumberSequence _seq;

    public LogisticUnitService(CodeMagDbContext db, INumberSequence seq)
    {
        _db = db;
        _seq = seq;
    }

    public async Task<List<LogisticUnit>> CreateManyAsync(
        Guid workOrderId,
        LogisticUnitType type,
        int count = 1,
        CancellationToken ct = default)
    {
        if (count <= 0) count = 1;
        if (count > 5000) count = 5000;

        // WorkOrder var mı?
        _ = await _db.WorkOrders.SingleAsync(x => x.Id == workOrderId, ct);

        var list = new List<LogisticUnit>(count);

        for (int i = 0; i < count; i++)
        {
            var next = await _seq.NextAsync("SSCC", ct);

            // 16 hane serial ref
            var serialRef16 = next.ToString("D16");

            // extension digit + payload = 17 hane
            var sscc17 = "0" + serialRef16;

            // check digit ekle = 18 hane
            var sscc18 = SsccHelper.AppendCheckDigit(sscc17);

            list.Add(new LogisticUnit
            {
                Id = Guid.NewGuid(),
                WorkOrderId = workOrderId,
                Type = type,          // Package / Pallet / Undefined
                SSCC = sscc18,
                CreatedAt = DateTime.UtcNow
            });
        }

        _db.LogisticUnits.AddRange(list);
        await _db.SaveChangesAsync(ct);

        return list;
    }
}
