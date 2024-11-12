using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class TemplateTypeConfiguration : IEntityTypeConfiguration<TemplateType>
{
    public void Configure(EntityTypeBuilder<TemplateType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("TemplateTypeId");
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(x => x.Description)
            .IsUnicode(false);
    }
}