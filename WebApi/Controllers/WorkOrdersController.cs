using Application.Services;
using Data.Db;
using Data.Entity;
using Data.Enum;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dto;
using WebApi.Dtos;

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

    [HttpGet]
    public async Task<ActionResult<List<WorkOrderListItemDto>>> GetAll(
        [FromServices] CodeMagDbContext db,
        CancellationToken ct)
    {
        var list = await db.WorkOrders
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new WorkOrderListItemDto
            {
                Id = x.Id,
                ProductId = x.Product.Id,
                ProductName = x.Product.Name,
                GTIN = x.Product.GTIN,
                BatchNo = x.BatchNo,
                ExpiryDate = x.ExpiryDate,
                Quantity = x.Quantity,
                ProducedCount = db.Serials.Count(s => s.WorkOrderId == x.Id),
                Status = (int)x.Status,
                SerialStart = (int)x.SerialStart,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(ct);

        return Ok(list);
    }

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
        return Ok();
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

    [HttpPost("{id:guid}/logistic-units")]
    public async Task<IActionResult> CreateLogisticUnits(
        Guid id,
        [FromServices] LogisticUnitService service,
        [FromQuery] LogisticUnitType type = LogisticUnitType.Package,
        [FromQuery] int count = 1,
        CancellationToken ct = default)
    {
        var units = await service.CreateManyAsync(id, type, count, ct);

        return Ok(new
        {
            WorkOrderId = id,
            GeneratedCount = units.Count,
            LogisticUnits = units.Select(u => new
            {
                u.Id,
                u.WorkOrderId,
                u.Type,
                u.SSCC,
                u.CreatedAt
            })
        });
    }

    [HttpGet("{id:guid}/detail")]
    public async Task<IActionResult> Detail(Guid id, [FromServices] WorkOrderDetailService svc)
    {
        var result = await svc.GetDetailAsync(id);
        return Ok(result);
    }

    [HttpPost("{id:guid}/serials/generate")]
    public async Task<IActionResult> GenerateSerials(
    Guid id,
    [FromServices] SerialService service,
    CancellationToken ct = default)
    {
        var serials = await service.GenerateAsync(id, 1, ct);

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


    [HttpPost("{id:guid}/serials")]
    public async Task<IActionResult> CreateSerials(
     Guid id,
     [FromServices] SerialService service,
     [FromQuery] int count = 1,
     CancellationToken ct = default)
    {
        var serials = await service.GenerateAsync(id, count, ct);

        return Ok(new
        {
            WorkOrderId = id,
            GeneratedCount = serials.Count,
            Serials = serials.Select(s => new
            {
                s.Id,
                s.WorkOrderId,
                s.GTIN,
                s.SerialNo,
                s.BatchNo,
                s.ExpiryDate,
                s.Gs1String,
                s.CreatedAt
            })
        });
    }

    [HttpGet("{id:guid}/snapshot")]
    public async Task<ActionResult<WorkOrderSnapshotDto>> Snapshot(
        Guid id,
        [FromServices] CodeMagDbContext db,
        CancellationToken ct)
    {
        // 1) WorkOrder + Product
        var wo = await db.WorkOrders
            .AsNoTracking()
            .Include(x => x.Product)
            .SingleOrDefaultAsync(x => x.Id == id, ct);

        if (wo == null) return NotFound(new { Message = $"WorkOrder not found: {id}" });

        // 2) Serials
        var serials = await db.Serials
            .AsNoTracking()
            .Where(s => s.WorkOrderId == id)
            .OrderBy(s => s.CreatedAt)
            .Select(s => new SerialDto
            {
                Id = s.Id,
                WorkOrderId = s.WorkOrderId,
                GTIN = s.GTIN,
                SerialNo = s.SerialNo,
                BatchNo = s.BatchNo,
                ExpiryDate = s.ExpiryDate,
                Gs1String = s.Gs1String,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync(ct);

        // 3) LogisticUnits (SSCC)
        var lus = await db.LogisticUnits
            .AsNoTracking()
            .Where(lu => lu.WorkOrderId == id)
            .OrderBy(lu => lu.CreatedAt)
            .Select(lu => new LogisticUnitDto
            {
                Id = lu.Id,
                WorkOrderId = lu.WorkOrderId,
                SSCC = lu.SSCC,
                Type = (int)lu.Type,
                CreatedAt = lu.CreatedAt
            })
            .ToListAsync(ct);

        // 4) AggregationLinks (bu workorder’ın LU’ları üzerinden)
        var luIds = lus.Select(x => x.Id).ToList();

        var links = luIds.Count == 0
            ? new List<AggregationLinkDto>()
            : await db.AggregationLinks
                .AsNoTracking()
                .Where(x => luIds.Contains(x.ParentLogisticUnitId))
                .OrderBy(x => x.CreatedAt)
                .Select(x => new AggregationLinkDto
                {
                    Id = x.Id,
                    ParentLogisticUnitId = x.ParentLogisticUnitId,
                    ChildType = (int)x.ChildType,
                    ChildLogisticUnitId = x.ChildLogisticUnitId,
                    ChildSerialId = x.ChildSerialId,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync(ct);

        var dto = new WorkOrderSnapshotDto
        {
            WorkOrder = new WorkOrderDto
            {
                Id = wo.Id,
                ProductId = wo.ProductId,
                BatchNo = wo.BatchNo,
                ExpiryDate = wo.ExpiryDate,
                Quantity = wo.Quantity,
                Status = (int)wo.Status,
                CreatedAt = wo.CreatedAt
            },
            Product = new ProductDto
            {
                Id = wo.Product.Id,
                CustomerId = wo.Product.CustomerId,
                GTIN = wo.Product.GTIN,
                Name = wo.Product.Name
            },
            Serials = serials,
            LogisticUnits = lus,
            AggregationLinks = links
        };

        return Ok(dto);
    }


}



