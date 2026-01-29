using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entity;
using WebApi.Enum;
using WebApi.Helpers;

namespace WebApi.Services;

public class LogisticUnitService
{
    private readonly CodeMagDbContext _db;

    // README varsayımı
    private const string ExtensionDigit = "0";
    private const string CompanyPrefix = "1234567"; // 7 hane örnek

    public LogisticUnitService(CodeMagDbContext db)
    {
        _db = db;
    }

    public async Task<LogisticUnit> CreateAsync(Guid workOrderId, LogisticUnitType type)
    {
        var woExists = await _db.WorkOrders.AnyAsync(x => x.Id == workOrderId);
        if (!woExists) throw new KeyNotFoundException("WorkOrder not found.");

        await using var tx = await _db.Database.BeginTransactionAsync();

        var seq = await _db.NumberSequences.FirstOrDefaultAsync(x => x.Key == "SSCC");
        if (seq == null)
        {
            seq = new NumberSequence { Key = "SSCC", NextValue = 1 };
            _db.NumberSequences.Add(seq);
            await _db.SaveChangesAsync();
        }

        var serialRef = seq.NextValue.ToString().PadLeft(17 - (ExtensionDigit.Length + CompanyPrefix.Length), '0');

        var sscc = SsccGenerator.BuildSscc(ExtensionDigit, CompanyPrefix, serialRef);

        seq.NextValue += 1;
        await _db.SaveChangesAsync();

        var lu = new LogisticUnit
        {
            Id = Guid.NewGuid(),
            WorkOrderId = workOrderId,
            Type = type,
            SSCC = sscc,
            CreatedAt = DateTime.UtcNow
        };

        _db.LogisticUnits.Add(lu);
        await _db.SaveChangesAsync();

        await tx.CommitAsync();
        return lu;
    }
}
