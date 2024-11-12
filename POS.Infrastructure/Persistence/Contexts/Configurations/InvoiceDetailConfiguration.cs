using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        builder.HasKey(x => new { x.InvoiceId, x.ProductServiceId });
        builder.Property(x => x.UnitPrice)
            .HasPrecision(10, 2);
        builder.Property(x => x.Total)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Invoice)
            .WithMany(y => y.InvoiceDetails)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.ProductService)
            .WithMany(y => y.InvoiceDetails)
            .HasForeignKey(x => x.ProductServiceId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}