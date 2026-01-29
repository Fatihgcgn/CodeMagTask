using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public sealed class AggregationLinkConfig : IEntityTypeConfiguration<AggregationLink>
{
    public void Configure(EntityTypeBuilder<AggregationLink> b)
    {
        b.ToTable("AggregationLinks");

        b.HasKey(x => x.Id);

        b.Property(x => x.ChildType).IsRequired();
        b.Property(x => x.CreatedAt).IsRequired();

        b.HasOne(x => x.ParentLogisticUnit)
            .WithMany(x => x.ParentLinks)
            .HasForeignKey(x => x.ParentLogisticUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.ChildLogisticUnit)
            .WithMany(x => x.ChildLinks)
            .HasForeignKey(x => x.ChildLogisticUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.ChildSerial)
            .WithMany()
            .HasForeignKey(x => x.ChildSerialId)
            .OnDelete(DeleteBehavior.Restrict);

        // Aynı child aynı parent’a bir kez bağlansın
        b.HasIndex(x => new { x.ParentLogisticUnitId, x.ChildLogisticUnitId })
            .IsUnique()
            .HasFilter("[ChildLogisticUnitId] IS NOT NULL");

        b.HasIndex(x => new { x.ParentLogisticUnitId, x.ChildSerialId })
            .IsUnique()
            .HasFilter("[ChildSerialId] IS NOT NULL");
    }
}
