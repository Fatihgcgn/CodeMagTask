using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entity;

namespace Data.Config;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> b)
    {
        b.ToTable("Customers");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.GLN)
            .IsRequired()
            .HasMaxLength(13);

        b.Property(x => x.Description)
            .HasMaxLength(500);

        b.HasIndex(x => x.GLN).IsUnique();
    }
}
