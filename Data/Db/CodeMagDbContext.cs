using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Data.Entity;

namespace Data.Db;

public class CodeMagDbContext : DbContext
{
    public CodeMagDbContext(DbContextOptions<CodeMagDbContext> options) : base(options) { }

    public DbSet<NextValueResult> NextValueResults => Set<NextValueResult>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<Serial> Serials => Set<Serial>();
    public DbSet<LogisticUnit> LogisticUnits => Set<LogisticUnit>();
    public DbSet<AggregationLink> AggregationLinks => Set<AggregationLink>();
    public DbSet<NumberSequence> NumberSequences => Set<NumberSequence>();

    public DbSet<PrintJobs> PrintJobs => Set<PrintJobs>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeMagDbContext).Assembly);

        modelBuilder.Entity<NextValueResult>().HasNoKey();

        base.OnModelCreating(modelBuilder);
    }

}