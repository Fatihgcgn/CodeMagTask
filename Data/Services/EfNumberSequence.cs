using Application.Abstractions;
using Data.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Data.Services;

public sealed class EfNumberSequence : INumberSequence
{
    private readonly CodeMagDbContext _db;
    public EfNumberSequence(CodeMagDbContext db) => _db = db;

    public async Task<long> NextAsync(string key, CancellationToken ct = default)
    {
        const string sql = @"
MERGE [NumberSequences] WITH (HOLDLOCK) AS target
USING (SELECT CAST(@key AS nvarchar(50)) AS [Key]) AS source
ON target.[Key] = source.[Key]
WHEN MATCHED THEN
    UPDATE SET [NextValue] = target.[NextValue] + 1
WHEN NOT MATCHED THEN
    INSERT ([Key], [NextValue]) VALUES (source.[Key], 1)
OUTPUT inserted.[NextValue];";

        var conn = _db.Database.GetDbConnection();   // ✅ DbContext'in connection'ı
        // ❌ conn'ı dispose ETME!

        if (conn.State != ConnectionState.Open)
            await conn.OpenAsync(ct);

        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;

        var p = cmd.CreateParameter();
        p.ParameterName = "@key";
        p.Value = key;
        cmd.Parameters.Add(p);

        // ✅ Eğer EF bir transaction içindeyse aynı transaction’a bağla
        var currentTx = _db.Database.CurrentTransaction;
        if (currentTx != null)
            cmd.Transaction = currentTx.GetDbTransaction();

        var result = await cmd.ExecuteScalarAsync(ct);

        return Convert.ToInt64(result);
    }

}
