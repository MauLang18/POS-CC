using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("UnitId");
        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .IsUnicode(false);
        builder.Property(x => x.Abbreviation)
            .HasMaxLength(7)
            .IsUnicode(false);
    }
}