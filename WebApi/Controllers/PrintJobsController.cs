using Data.Db;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PrintJobsController : ControllerBase
{
    private readonly CodeMagDbContext _db;

    public PrintJobsController(CodeMagDbContext db)
    {
        _db = db;
    }

    // GET api/printjobs?workOrderId=...&targetType=Serial
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid workOrderId, [FromQuery] string? targetType = null, CancellationToken ct = default)
    {
        if (workOrderId == Guid.Empty) return BadRequest("workOrderId is required.");

        var q = _db.PrintJobs.AsNoTracking()
            .Where(x => x.WorkOrderId == workOrderId);

        if (!string.IsNullOrWhiteSpace(targetType))
            q = q.Where(x => x.TargetType == targetType);

        var list = await q
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new
            {
                x.Id,
                x.WorkOrderId,
                x.TargetType,
                x.TargetId,
                x.Payload,
                x.Status,
                x.CreatedAt,
                x.PrintedAt,
                x.Error
            })
            .ToListAsync(ct);

        return Ok(list);
    }

    // POST api/printjobs/{id}/mark-printed
    [HttpPost("{id:guid}/mark-printed")]
    public async Task<IActionResult> MarkPrinted(Guid id, CancellationToken ct = default)
    {
        var job = await _db.PrintJobs.SingleOrDefaultAsync(x => x.Id == id, ct);
        if (job == null) return NotFound();

        if (job.Status == "Printed") return Ok(); // idempotent

        job.Status = "Printed";
        job.PrintedAt = DateTime.UtcNow;
        job.Error = null;

        await _db.SaveChangesAsync(ct);

        return Ok(new
        {
            job.Id,
            job.Status,
            job.PrintedAt
        });
    }

    public sealed class MarkFailedRequest
    {
        public string? Error { get; set; }
    }

    // POST api/printjobs/{id}/mark-failed   body: { "error": "..." }
    [HttpPost("{id:guid}/mark-failed")]
    public async Task<IActionResult> MarkFailed(Guid id, [FromBody] MarkFailedRequest req, CancellationToken ct = default)
    {
        var job = await _db.PrintJobs.SingleOrDefaultAsync(x => x.Id == id, ct);
        if (job == null) return NotFound();

        job.Status = "Failed";
        job.Error = string.IsNullOrWhiteSpace(req?.Error) ? "Simulated error" : req!.Error;
        job.PrintedAt = null;

        await _db.SaveChangesAsync(ct);

        return Ok(new { job.Id, job.Status, job.Error });
    }
}