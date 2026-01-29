using Application.Exceptions; // namespace’ini nereye koyduysan ona göre düzelt
using Data.Db;
using Data.Entity;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using static Application.Exceptions.DomainExceptions;

namespace Data.Services;

public sealed class AggregationService
{
    private readonly CodeMagDbContext _db;
    public AggregationService(CodeMagDbContext db) => _db = db;

    // Parent = Package, Child = Serial
    public async Task<AggregationLink> AddSerialAsync(Guid parentLuId, Guid serialId, CancellationToken ct = default)
    {
        await using var tx = await _db.Database.BeginTransactionAsync(ct);

        var parent = await _db.LogisticUnits.SingleOrDefaultAsync(x => x.Id == parentLuId, ct);
        if (parent == null) throw new NotFoundException($"Parent logistic unit not found: {parentLuId}");

        if (parent.Type != LogisticUnitType.Package)
            throw new ValidationException($"Parent must be Package to add Serial. ParentType={parent.Type}");

        var serial = await _db.Serials.SingleOrDefaultAsync(x => x.Id == serialId, ct);
        if (serial == null) throw new NotFoundException($"Serial not found: {serialId}");

        // Opsiyonel: aynı workorder mı?
        if (serial.WorkOrderId != parent.WorkOrderId)
            throw new ValidationException("Serial and Package must belong to same WorkOrder.");

        // Serial zaten bir yere bağlı mı? (childSerial unique index'in varsa DB de korur ama service de kontrol edelim)
        var alreadyLinked = await _db.AggregationLinks
            .AnyAsync(x => x.ChildSerialId == serialId, ct);
        if (alreadyLinked)
            throw new ConflictException($"Serial already aggregated: {serialId}");

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
            await tx.CommitAsync(ct);
            return link;
        }
        catch (DbUpdateException)
        {
            // Unique index çakışmaları vb.
            throw new ConflictException("Aggregation already exists or violates uniqueness constraints.");
        }
    }

    // Parent = Pallet, Child = Package
    public async Task<AggregationLink> AddPackageToPalletAsync(Guid palletId, Guid packageId, CancellationToken ct = default)
    {
        await using var tx = await _db.Database.BeginTransactionAsync(ct);

        var pallet = await _db.LogisticUnits.SingleOrDefaultAsync(x => x.Id == palletId, ct);
        if (pallet == null) throw new NotFoundException($"Pallet not found: {palletId}");

        if (pallet.Type != LogisticUnitType.Pallet)
            throw new ValidationException($"Parent must be Pallet to add Package. ParentType={pallet.Type}");

        var package = await _db.LogisticUnits.SingleOrDefaultAsync(x => x.Id == packageId, ct);
        if (package == null) throw new NotFoundException($"Package not found: {packageId}");

        if (package.Type != LogisticUnitType.Package)
            throw new ValidationException($"Child must be Package. ChildType={package.Type}");

        if (package.WorkOrderId != pallet.WorkOrderId)
            throw new ValidationException("Package and Pallet must belong to same WorkOrder.");

        // Package zaten bir palete bağlı mı?
        var alreadyLinked = await _db.AggregationLinks
            .AnyAsync(x => x.ChildLogisticUnitId == packageId, ct);
        if (alreadyLinked)
            throw new ConflictException($"Package already aggregated: {packageId}");

        var link = new AggregationLink
        {
            Id = Guid.NewGuid(),
            ParentLogisticUnitId = palletId,
            ChildType = AggregationChildType.LogisticUnit,
            ChildLogisticUnitId = packageId,
            ChildSerialId = null,
            CreatedAt = DateTime.UtcNow
        };

        _db.AggregationLinks.Add(link);

        try
        {
            await _db.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
            return link;
        }
        catch (DbUpdateException)
        {
            throw new ConflictException("Aggregation already exists or violates uniqueness constraints.");
        }
    }
}
