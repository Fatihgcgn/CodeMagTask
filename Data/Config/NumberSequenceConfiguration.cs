using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public sealed class NumberSequenceConfig : IEntityTypeConfiguration<NumberSequence>
{
    public void Configure(EntityTypeBuilder<NumberSequence> b)
    {
        b.ToTable("NumberSequences");

        b.HasKey(x => x.Key);

        b.Property(x => x.Key)
            .HasMaxLength(50)
            .IsRequired();

        b.Property(x => x.NextValue)
            .IsRequired();
    }
}
