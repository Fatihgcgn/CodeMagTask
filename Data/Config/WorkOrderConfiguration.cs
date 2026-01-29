using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entity;

namespace Data.Config;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> b)
    {
        b.ToTable("WorkOrders");

        b.HasKey(x => x.Id);

        b.Property(x => x.Quantity)
            .IsRequired();

        b.Property(x => x.BatchNo)
            .IsRequired()
            .HasMaxLength(30);

        b.Property(x => x.ExpiryDate)
            .IsRequired();

        b.Property(x => x.SerialStart)
            .IsRequired();

        b.Property(x => x.Status)
            .IsRequired();

        b.Property(x => x.CreatedAt)
            .IsRequired();

        b.HasOne(x => x.Product)
            .WithMany(p => p.WorkOrders)
            .HasForeignKey(x => x.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}