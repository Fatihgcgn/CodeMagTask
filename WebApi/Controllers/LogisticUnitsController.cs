using Application.Services;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogisticUnitsController : ControllerBase
    {
        [HttpPost("{parentId:guid}/add-serial")]
        public async Task<IActionResult> AddSerial(Guid parentId, [FromBody] AddSerialToLogisticUnitRequest req,
            [FromServices] AggregationService service)
        {
            var link = await service.AddSerialAsync(parentId, req.SerialId);
            return Ok(new
            {
                link.Id,
                link.ParentLogisticUnitId,
                link.ChildType,
                link.ChildSerialId,
                link.CreatedAt
            });
        }

        [HttpPost("{palletId:guid}/add-package")]
        public async Task<IActionResult> AddPackage(
        Guid palletId,
        [FromBody] AddPackageToPalletRequest req,
        [FromServices] AggregationService service)
        {
            var link = await service.AddChildUnitAsync(palletId, req.PackageId);

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

}
