using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Enum;

namespace WebApi.Services;

public class AggregationService
{
    private readonly CodeMagDbContext _db;

    public AggregationService(CodeMagDbContext db)
    {
        _db = db;
    }

    public async Task<AggregationLink> AddSerialAsync(Guid parentLogisticUnitId, Guid serialId)
    {
        var parent = await _db.LogisticUnits.FirstOrDefaultAsync(x => x.Id == parentLogisticUnitId);
        if (parent == null) throw new KeyNotFoundException("Parent logistic unit bulunamadı.");

        if (parent.Type != LogisticUnitType.Package)
            throw new InvalidOperationException("Sadece Paket serileri barındırabilir.");

        var serial = await _db.Serials.FirstOrDefaultAsync(x => x.Id == serialId);
        if (serial == null) throw new KeyNotFoundException("Serial not found.");

        if (serial.WorkOrderId != parent.WorkOrderId)
            throw new InvalidOperationException("Serial ve Package mutlaka aynı Work Order'da bulunmalı.");

        var link = new AggregationLink
        {
            Id = Guid.NewGuid(),
            ParentLogisticUnitId = parent.Id,
            ChildType = AggregationChildType.Serial,
            ChildSerialId = serial.Id,
            ChildLogisticUnitId = null,
            CreatedAt = DateTime.UtcNow
        };

        _db.AggregationLinks.Add(link);
        await _db.SaveChangesAsync();

        return link;
    }

    public async Task<AggregationLink> AddPackageToPalletAsync(Guid palletId, Guid packageId)
    {
        var pallet = await _db.LogisticUnits.FirstOrDefaultAsync(x => x.Id == palletId);
        if (pallet == null) throw new KeyNotFoundException("Pallet not found.");
        if (pallet.Type != LogisticUnitType.Pallet)
            throw new InvalidOperationException("Parent must be Pallet.");

        var pack = await _db.LogisticUnits.FirstOrDefaultAsync(x => x.Id == packageId);
        if (pack == null) throw new KeyNotFoundException("Package not found.");
        if (pack.Type != LogisticUnitType.Package)
            throw new InvalidOperationException("Child must be Package.");

        if (pallet.WorkOrderId != pack.WorkOrderId)
            throw new InvalidOperationException("Pallet and Package must belong to same WorkOrder.");

        var link = new AggregationLink
        {
            Id = Guid.NewGuid(),
            ParentLogisticUnitId = pallet.Id,
            ChildType = AggregationChildType.LogisticUnit,
            ChildLogisticUnitId = pack.Id,
            ChildSerialId = null,
            CreatedAt = DateTime.UtcNow
        };

        _db.AggregationLinks.Add(link);
        await _db.SaveChangesAsync();
        return link;

    }

}
