using Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/logistic-units")]
public class AggregationController : ControllerBase
{
    // POST /api/logistic-units/{parentId}/aggregate/serial/{serialId}
    [HttpPost("{parentId:guid}/aggregate/serial/{serialId:guid}")]
    public async Task<IActionResult> AggregateSerial(
        Guid parentId,
        Guid serialId,
        [FromServices] AggregationService service,
        CancellationToken ct = default)
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

    // POST /api/logistic-units/{parentId}/aggregate/unit/{childLuId}
    [HttpPost("{parentId:guid}/aggregate/unit/{childLuId:guid}")]
    public async Task<IActionResult> AggregateUnit(
        Guid parentId,
        Guid childLuId,
        [FromServices] AggregationService service,
        CancellationToken ct = default)
    {
        var link = await service.AddChildUnitAsync(parentId, childLuId, ct);

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
