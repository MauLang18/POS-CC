using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class DocumentTemplateConfiguration : IEntityTypeConfiguration<DocumentTemplate>
{
    public void Configure(EntityTypeBuilder<DocumentTemplate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("DocumentTemplateId");
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(x => x.Content)
            .IsUnicode(false);
        builder.HasOne(x => x.TemplateType)
            .WithMany(y => y.DocumentTemplates)
            .HasForeignKey(x => x.TemplateTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}