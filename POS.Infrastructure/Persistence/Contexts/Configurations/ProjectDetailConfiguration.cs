using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class ProjectDetailConfiguration : IEntityTypeConfiguration<ProjectDetail>
{
    public void Configure(EntityTypeBuilder<ProjectDetail> builder)
    {
        builder.HasKey(x => new { x.ProjectId, x.Id });
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Requirement)
            .IsUnicode(false);
        builder.HasOne(x => x.Project)
            .WithMany(y => y.ProjectDetails)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Status)
            .WithMany(y => y.ProjectDetails)
            .HasForeignKey(x => x.StateId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}