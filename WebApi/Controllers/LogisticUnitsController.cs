using Application.Exceptions;
using Data.Db;
using Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Application.Exceptions.DomainExceptions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/logistic-units")]
public sealed class LogisticUnitsQueryController : ControllerBase
{
    private readonly CodeMagDbContext _db;
    public LogisticUnitsQueryController(CodeMagDbContext db) => _db = db;

    [HttpGet("{id:guid}/tree")]
    public async Task<IActionResult> GetTree(Guid id, CancellationToken ct)
    {
        var root = await _db.LogisticUnits.SingleOrDefaultAsync(x => x.Id == id, ct);
        if (root == null) throw new NotFoundException($"LogisticUnit not found: {id}");

        if (root.Type != LogisticUnitType.Pallet)
            throw new ValidationException("Tree endpoint expects a Pallet id.");

        // 1) Pallet'in child package linkleri
        var packageLinks = await _db.AggregationLinks
            .Where(x => x.ParentLogisticUnitId == id && x.ChildLogisticUnitId != null)
            .ToListAsync(ct);

        var packageIds = packageLinks.Select(x => x.ChildLogisticUnitId!.Value).Distinct().ToList();

        var packages = await _db.LogisticUnits
            .Where(x => packageIds.Contains(x.Id))
            .ToListAsync(ct);

        // 2) Bu package’ların altındaki serial linkleri
        var serialLinks = await _db.AggregationLinks
            .Where(x => packageIds.Contains(x.ParentLogisticUnitId) && x.ChildSerialId != null)
            .ToListAsync(ct);

        var serialIds = serialLinks.Select(x => x.ChildSerialId!.Value).Distinct().ToList();

        var serials = await _db.Serials
            .Where(x => serialIds.Contains(x.Id))
            .ToListAsync(ct);

        // 3) Shape et
        var result = new
        {
            Pallet = new
            {
                root.Id,
                root.SSCC,
                Type = root.Type.ToString(),
                Packages = packages.Select(p =>
                {
                    var pSerialIds = serialLinks
                        .Where(l => l.ParentLogisticUnitId == p.Id)
                        .Select(l => l.ChildSerialId!.Value)
                        .ToHashSet();

                    var pSerials = serials.Where(s => pSerialIds.Contains(s.Id));

                    return new
                    {
                        p.Id,
                        p.SSCC,
                        Type = p.Type.ToString(),
                        Serials = pSerials.Select(s => new
                        {
                            s.Id,
                            s.GTIN,
                            s.SerialNo,
                            s.BatchNo,
                            s.ExpiryDate,
                            s.Gs1String
                        })
                    };
                })
            }
        };

        return Ok(result);
    }
}
