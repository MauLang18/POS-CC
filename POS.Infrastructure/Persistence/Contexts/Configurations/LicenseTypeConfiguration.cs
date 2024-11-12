using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class LicenseTypeConfiguration : IEntityTypeConfiguration<LicenseType>
{
    public void Configure(EntityTypeBuilder<LicenseType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("LicenseTypeId");
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(x => x.Description)
            .IsUnicode(false);
    }
}