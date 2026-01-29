using Data.Db;
using Data.Entity;
using Data.Enum;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public sealed class AggregationService
{
    private readonly CodeMagDbContext _db;
    public AggregationService(CodeMagDbContext db) => _db = db;

    public async Task<AggregationLink> AddSerialAsync(Guid parentLuId, Guid serialId, CancellationToken ct = default)
    {
        // Parent var mı?
        _ = await _db.LogisticUnits.SingleAsync(x => x.Id == parentLuId, ct);

        // Serial var mı?
        _ = await _db.Serials.SingleAsync(x => x.Id == serialId, ct);

        // Aynı parent-child zaten var mı?
        var exists = await _db.AggregationLinks.AnyAsync(x =>
            x.ParentLogisticUnitId == parentLuId &&
            x.ChildSerialId == serialId, ct);

        if (exists)
            throw new InvalidOperationException("This serial is already aggregated under the parent logistic unit.");

        var link = new AggregationLink
        {
            Id = Guid.NewGuid(),
            ParentLogisticUnitId = parentLuId,
            ChildType = AggregationChildType.Serial,
            ChildSerialId = serialId,
            ChildLogisticUnitId = null,
            CreatedAt = DateTime.UtcNow
        };

        _db.AggregationLinks.Add(link);

        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            // Unique index aynı anda 2 istek gelirse patlayabilir → burada düzgün hata dön
            throw new InvalidOperationException("Aggregation already exists (concurrent request).");
        }

        return link;
    }

    public async Task<AggregationLink> AddChildUnitAsync(Guid parentLuId, Guid childLuId, CancellationToken ct = default)
    {
        if (parentLuId == childLuId)
            throw new InvalidOperationException("A logistic unit cannot be aggregated into itself.");

        _ = await _db.LogisticUnits.SingleAsync(x => x.Id == parentLuId, ct);
        _ = await _db.LogisticUnits.SingleAsync(x => x.Id == childLuId, ct);

        var exists = await _db.AggregationLinks.AnyAsync(x =>
            x.ParentLogisticUnitId == parentLuId &&
            x.ChildLogisticUnitId == childLuId, ct);

        if (exists)
            throw new InvalidOperationException("This logistic unit is already aggregated under the parent logistic unit.");

        var link = new AggregationLink
        {
            Id = Guid.NewGuid(),
            ParentLogisticUnitId = parentLuId,
            ChildType = AggregationChildType.LogisticUnit,
            ChildLogisticUnitId = childLuId,
            ChildSerialId = null,
            CreatedAt = DateTime.UtcNow
        };

        _db.AggregationLinks.Add(link);

        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Aggregation already exists (concurrent request).");
        }

        return link;
    }
}
