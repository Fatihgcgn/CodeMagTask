using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entity;

namespace Data.Config;

public class SerialConfiguration : IEntityTypeConfiguration<Serial>
{
    public void Configure(EntityTypeBuilder<Serial> b)
    {
        b.ToTable("Serials");

        b.HasKey(x => x.Id);

        b.Property(x => x.GTIN)
            .IsRequired()
            .HasMaxLength(14);

        b.Property(x => x.SerialNo)
            .IsRequired()
            .HasMaxLength(50);

        b.Property(x => x.BatchNo)
            .IsRequired()
            .HasMaxLength(30);

        b.Property(x => x.ExpiryDate)
            .IsRequired();

        b.Property(x => x.Gs1String)
            .IsRequired()
            .HasMaxLength(256);

        b.Property(x => x.CreatedAt)
            .IsRequired();

        b.HasOne(x => x.WorkOrder)
            .WithMany(w => w.Serials)
            .HasForeignKey(x => x.WorkOrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => new { x.GTIN, x.SerialNo }).IsUnique();

    }
}