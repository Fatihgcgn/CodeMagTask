using Microsoft.EntityFrameworkCore;
using WebApi.Entity;

namespace WebApi.Data;

public class CodeMagDbContext : DbContext
{
    public CodeMagDbContext(DbContextOptions<CodeMagDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<Serial> Serials => Set<Serial>();
    public DbSet<LogisticUnit> LogisticUnits => Set<LogisticUnit>();
    public DbSet<AggregationLink> AggregationLinks => Set<AggregationLink>();
    public DbSet<NumberSequence> NumberSequences => Set<NumberSequence>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeMagDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}