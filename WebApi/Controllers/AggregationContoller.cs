using Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/logistic-units")]
public class AggregationController : ControllerBase
{
    [HttpPost("{parentId:guid}/aggregate/serial/{serialId:guid}")]
    public async Task<IActionResult> AggregateSerial(
        Guid parentId,
        Guid serialId,
        [FromServices] AggregationService service,
        CancellationToken ct)
    {
        var link = await service.AddSerialAsync(parentId, serialId, ct);
        return Ok(new
        {
            link.Id,
            link.ParentLogisticUnitId,
            link.ChildType,
            link.ChildSerialId,
            link.CreatedAt
        });
    }

    [HttpPost("{palletId:guid}/aggregate/package/{packageId:guid}")]
    public async Task<IActionResult> AggregatePackage(
        Guid palletId,
        Guid packageId,
        [FromServices] AggregationService service,
        CancellationToken ct)
    {
        var link = await service.AddPackageToPalletAsync(palletId, packageId, ct);
        return Ok(new
        {
            link.Id,
            link.ParentLogisticUnitId,
            link.ChildType,
            link.ChildLogisticUnitId,
            link.CreatedAt
        });
    }


}
