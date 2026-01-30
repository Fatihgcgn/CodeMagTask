using Data.Db;
using Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/plc/workorders")]
public sealed class PlcController : ControllerBase
{
    private readonly CodeMagDbContext _db;

    public PlcController(CodeMagDbContext db)
    {
        _db = db;
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(Guid id, CancellationToken ct)
    {
        var wo = await _db.WorkOrders.SingleOrDefaultAsync(x => x.Id == id, ct);
        if (wo == null) return NotFound();

        if (wo.Status == WorkOrderStatus.Serializing)
            return Ok(); // idempotent

        if (wo.Status != WorkOrderStatus.Created &&
            wo.Status != WorkOrderStatus.Serialized)
            return BadRequest($"Cannot start from status {wo.Status}");

        wo.Status = WorkOrderStatus.Serializing;
        await _db.SaveChangesAsync(ct);

        return Ok(new { wo.Id, Status = wo.Status.ToString() });
    }

    [HttpPost("{id:guid}/stop")]
    public async Task<IActionResult> Stop(Guid id, CancellationToken ct)
    {
        var wo = await _db.WorkOrders.SingleOrDefaultAsync(x => x.Id == id, ct);
        if (wo == null) return NotFound();

        if (wo.Status != WorkOrderStatus.Serializing)
            return BadRequest("Only Serializing can be stopped.");

        wo.Status = WorkOrderStatus.Serialized;
        await _db.SaveChangesAsync(ct);

        return Ok(new { wo.Id, Status = wo.Status.ToString() });
    }
}