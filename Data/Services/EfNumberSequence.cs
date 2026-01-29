using Application.Abstractions;
using Data.Db;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public sealed class EfNumberSequence : INumberSequence
{
    private readonly CodeMagDbContext _db;
    public EfNumberSequence(CodeMagDbContext db) => _db = db;

    public async Task<long> NextAsync(string key, CancellationToken ct = default)
    {
        const string sql = @"
MERGE [NumberSequences] WITH (HOLDLOCK) AS target
USING (SELECT CAST({0} AS nvarchar(50)) AS [Key]) AS source
ON target.[Key] = source.[Key]
WHEN MATCHED THEN
    UPDATE SET [NextValue] = target.[NextValue] + 1
WHEN NOT MATCHED THEN
    INSERT ([Key], [NextValue]) VALUES (source.[Key], 1)
OUTPUT inserted.[NextValue] AS NextValue;";

        var result = await _db.Set<NextValueResult>()
            .FromSqlRaw(sql, key)
            .SingleAsync(ct);

        return result.NextValue;
    }


}
