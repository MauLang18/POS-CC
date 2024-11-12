using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class LicenseConfiguration : IEntityTypeConfiguration<License>
{
    public void Configure(EntityTypeBuilder<License> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("LicenseId");
        builder.Property(x => x.LicenseKey)
            .IsUnicode(false);
        builder.HasOne(x => x.Project)
            .WithMany(y => y.Licenses)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.LicenseType)
            .WithMany(y => y.Licenses)
            .HasForeignKey(x => x.LicenseTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}