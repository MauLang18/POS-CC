using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("EmailTemplateId");
        builder.Property(x => x.Subject)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(x => x.Body)
            .IsUnicode(false);
        builder.HasOne(x => x.TemplateType)
            .WithMany(y => y.EmailTemplates)
            .HasForeignKey(x => x.TemplateTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}