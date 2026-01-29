using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class WorkOrdersController : Controller
    {

        [HttpPost("{id:guid}/logistic-units")]
        public async Task<IActionResult> CreateLogisticUnit(Guid id, [FromBody] CreateLogisticUnitRequest req,
    [FromServices] LogisticUnitService service)
        {
            var lu = await service.CreateAsync(id, req.Type);

            return Ok(new
            {
                lu.Id,
                lu.WorkOrderId,
                lu.Type,
                lu.SSCC,
                lu.CreatedAt
            });
        }
    }
}
