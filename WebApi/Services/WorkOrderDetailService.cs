using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Enum;

namespace WebApi.Services;

public class WorkOrderDetailService
{
    private readonly CodeMagDbContext _db;
    public WorkOrderDetailService(CodeMagDbContext db) => _db = db;

    public async Task<object> GetDetailAsync(Guid workOrderId)
    {
        var wo = await _db.WorkOrders
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == workOrderId);

        if (wo == null) throw new KeyNotFoundException("WorkOrder bulunamadı.");

        var serials = await _db.Serials
            .Where(s => s.WorkOrderId == workOrderId)
            .Select(s => new
            {
                s.Id,
                s.GTIN,
                s.SerialNo,
                s.BatchNo,
                s.ExpiryDate,
                s.Gs1String
            })
            .ToListAsync();

        var units = await _db.LogisticUnits
            .Where(lu => lu.WorkOrderId == workOrderId)
            .Select(lu => new { lu.Id, lu.Type, lu.SSCC, lu.CreatedAt })
            .ToListAsync();

        var links = await _db.AggregationLinks
            .Where(l => _db.LogisticUnits.Any(lu => lu.Id == l.ParentLogisticUnitId && lu.WorkOrderId == workOrderId))
            .Select(l => new
            {
                l.Id,
                l.ParentLogisticUnitId,
                l.ChildType,
                l.ChildSerialId,
                l.ChildLogisticUnitId
            })
            .ToListAsync();

        // Hiyerarşiyi memory'de kur (basit)
        var packageMap = units.Where(u => u.Type == LogisticUnitType.Package)
            .ToDictionary(u => u.Id, u => new
            {
                u.Id,
                u.SSCC,
                Serials = new List<object>()
            });

        foreach (var l in links.Where(x => x.ChildType == AggregationChildType.Serial && x.ChildSerialId != null))
        {
            if (packageMap.TryGetValue(l.ParentLogisticUnitId, out var pack))
            {
                var s = serials.FirstOrDefault(x => x.Id == l.ChildSerialId);
                if (s != null) pack.Serials.Add(s);
            }
        }

        var palletMap = units.Where(u => u.Type == LogisticUnitType.Pallet)
            .ToDictionary(u => u.Id, u => new
            {
                u.Id,
                u.SSCC,
                Packages = new List<object>()
            });

        foreach (var l in links.Where(x => x.ChildType == AggregationChildType.LogisticUnit && x.ChildLogisticUnitId != null))
        {
            if (palletMap.TryGetValue(l.ParentLogisticUnitId, out var pal) &&
                packageMap.TryGetValue(l.ChildLogisticUnitId.Value, out var pack))
            {
                pal.Packages.Add(pack);
            }
        }

        return new
        {
            WorkOrder = new
            {
                wo.Id,
                wo.Quantity,
                wo.BatchNo,
                wo.ExpiryDate,
                wo.SerialStart,
                wo.Status,
                wo.CreatedAt
            },
            Product = new
            {
                wo.Product.Id,
                wo.Product.GTIN,
                wo.Product.Name,
                wo.Product.CustomerId
            },
            Serials = serials,
            LogisticUnits = units,
            Aggregation = new
            {
                Packages = packageMap.Values,
                Pallets = palletMap.Values
            }
        };
    }
}
