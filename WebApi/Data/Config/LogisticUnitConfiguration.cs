using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity;

namespace WebApi.Data.Config;

public class LogisticUnitConfiguration : IEntityTypeConfiguration<LogisticUnit>
{
    public void Configure(EntityTypeBuilder<LogisticUnit> b)
    {
        b.ToTable("LogisticUnits");

        b.HasKey(x => x.Id);

        b.Property(x => x.SSCC)
            .IsRequired()
            .HasMaxLength(18);

        b.Property(x => x.Type)
            .IsRequired();

        b.Property(x => x.CreatedAt)
            .IsRequired();

        b.HasOne(x => x.WorkOrder)
            .WithMany(w => w.LogisticUnits)
            .HasForeignKey(x => x.WorkOrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => x.SSCC).IsUnique();
    }
}
