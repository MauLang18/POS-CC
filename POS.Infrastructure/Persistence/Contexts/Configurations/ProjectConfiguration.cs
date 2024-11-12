using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("ProjectId");
        builder.Property(x => x.InternalName)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(x => x.CommercialName)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.HasOne(x => x.Customer)
            .WithMany(y => y.Projects)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Category)
            .WithMany(y => y.Projects)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Status)
            .WithMany(y => y.Projects)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}