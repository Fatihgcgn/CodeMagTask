using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Entity;
using WebApi.Enum;      // senin enum klasörün
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/workorders")]
public class WorkOrdersController : ControllerBase
{
    private readonly CodeMagDbContext _db;

    public WorkOrdersController(CodeMagDbContext db)
    {
        _db = db;
    }


    // ---------------------------

    // CREATE: Product'a bağlı WorkOrder
    [HttpPost("/api/products/{productId:guid}/workorders")]
    public async Task<IActionResult> Create(Guid productId, [FromBody] CreateWorkOrderRequest req)
    {
        if (req.Quantity <= 0) return BadRequest("Quantity must be > 0.");
        if (req.SerialStart <= 0) return BadRequest("SerialStart must be > 0.");
        if (string.IsNullOrWhiteSpace(req.BatchNo)) return BadRequest("BatchNo is required.");

        var product = await _db.Products.FindAsync(productId);
        if (product == null) return NotFound("Product not found.");

        var wo = new WorkOrder
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Quantity = req.Quantity,
            BatchNo = req.BatchNo,
            ExpiryDate = req.ExpiryDate,
            SerialStart = req.SerialStart,
            Status = req.Status ?? WorkOrderStatus.Created,
            CreatedAt = DateTime.UtcNow
        };

        _db.WorkOrders.Add(wo);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            wo.Id,
            wo.ProductId,
            wo.Quantity,
            wo.BatchNo,
            wo.ExpiryDate,
            wo.SerialStart,
            wo.Status,
            wo.CreatedAt
        });
    }

    // LIST: Product'ın WorkOrder'ları
    [HttpGet("/api/products/{productId:guid}/workorders")]
    public async Task<IActionResult> GetByProduct(Guid productId)
    {
        var exists = await _db.Products.AnyAsync(x => x.Id == productId);
        if (!exists) return NotFound("Product not found.");

        var list = await _db.WorkOrders
            .Where(x => x.ProductId == productId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new
            {
                x.Id,
                x.Quantity,
                x.BatchNo,
                x.ExpiryDate,
                x.SerialStart,
                x.Status,
                x.CreatedAt
            })
            .ToListAsync();

        return Ok(list);
    }

    // GET: Tek WorkOrder
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var wo = await _db.WorkOrders
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (wo == null) return NotFound();

        return Ok(new
        {
            wo.Id,
            wo.ProductId,
            wo.Quantity,
            wo.BatchNo,
            wo.ExpiryDate,
            wo.SerialStart,
            wo.Status,
            wo.CreatedAt,
            Product = new { wo.Product.Id, wo.Product.GTIN, wo.Product.Name, wo.Product.Description }
        });
    }

    // UPDATE
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkOrderRequest req)
    {
        if (req.Quantity <= 0) return BadRequest("Quantity must be > 0.");
        if (req.SerialStart <= 0) return BadRequest("SerialStart must be > 0.");
        if (string.IsNullOrWhiteSpace(req.BatchNo)) return BadRequest("BatchNo is required.");

        var wo = await _db.WorkOrders.FindAsync(id);
        if (wo == null) return NotFound();

        // serial üretildiyse kritik alanları kilitle (case için gerçekçi)
        var hasSerials = await _db.Serials.AnyAsync(s => s.WorkOrderId == id);
        if (hasSerials)
            return Conflict("Cannot update WorkOrder fields after serials generated.");

        wo.Quantity = req.Quantity;
        wo.BatchNo = req.BatchNo;
        wo.ExpiryDate = req.ExpiryDate;
        wo.SerialStart = req.SerialStart;
        wo.Status = req.Status;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var wo = await _db.WorkOrders.FindAsync(id);
        if (wo == null) return NotFound();

        var hasSerials = await _db.Serials.AnyAsync(s => s.WorkOrderId == id);
        var hasUnits = await _db.LogisticUnits.AnyAsync(lu => lu.WorkOrderId == id);

        if (hasSerials || hasUnits)
            return Conflict("Cannot delete WorkOrder after serialization/aggregation started.");

        _db.WorkOrders.Remove(wo);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------

    // Create Logistic Unit (Package/Pallet)
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

    // WorkOrder Detail (case zorunlu endpoint)
    [HttpGet("{id:guid}/detail")]
    public async Task<IActionResult> Detail(Guid id, [FromServices] WorkOrderDetailService svc)
    {
        var result = await svc.GetDetailAsync(id);
        return Ok(result);
    }

    [HttpPost("{id:guid}/serials/generate")]
    public async Task<IActionResult> GenerateSerials(Guid id, [FromServices] SerialService service)
    {
        var serials = await service.GenerateAsync(id);

        return Ok(new
        {
            WorkOrderId = id,
            GeneratedCount = serials.Count,
            Serials = serials.Select(s => new
            {
                s.Id,
                s.GTIN,
                s.SerialNo,
                s.BatchNo,
                s.ExpiryDate,
                s.Gs1String
            })
        });
    }
}
