using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity;

namespace WebApi.Data.Config;

public class AggregationLinkConfiguration : IEntityTypeConfiguration<AggregationLink>
{
    public void Configure(EntityTypeBuilder<AggregationLink> b)
    {
        b.ToTable("AggregationLinks");

        b.HasKey(x => x.Id);

        b.Property(x => x.ChildType)
            .IsRequired();

        b.Property(x => x.CreatedAt)
            .IsRequired();

        b.HasOne(x => x.ParentLogisticUnit)
            .WithMany(lu => lu.ParentLinks)
            .HasForeignKey(x => x.ParentLogisticUnitId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.ChildLogisticUnit)
            .WithMany(lu => lu.ChildLinks)
            .HasForeignKey(x => x.ChildLogisticUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.ChildSerial)
            .WithMany()
            .HasForeignKey(x => x.ChildSerialId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => x.ParentLogisticUnitId);

        b.HasIndex(x => new { x.ParentLogisticUnitId, x.ChildSerialId }).IsUnique();
        b.HasIndex(x => new { x.ParentLogisticUnitId, x.ChildLogisticUnitId }).IsUnique();

    }
}