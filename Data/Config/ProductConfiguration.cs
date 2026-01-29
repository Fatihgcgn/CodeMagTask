using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entity;

namespace Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> b)
    {
        b.ToTable("Products");

        b.HasKey(x => x.Id);

        b.Property(x => x.GTIN)
            .IsRequired()
            .HasMaxLength(14);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Description)
        .HasMaxLength(500);

        b.HasOne(x => x.Customer)
            .WithMany(c => c.Products)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => new { x.CustomerId, x.GTIN }).IsUnique();
    }
}
