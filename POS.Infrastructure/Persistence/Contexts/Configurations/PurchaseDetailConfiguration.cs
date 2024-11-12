using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
{
    public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
    {
        builder.HasKey(x => new { x.PurchaseId, x.ProductServiceId });
        builder.Property(x => x.UnitPrice)
            .HasPrecision(10, 2);
        builder.Property(x => x.Total)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Purchase)
            .WithMany(y => y.PurchaseDetails)
            .HasForeignKey(x => x.PurchaseId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.ProductService)
            .WithMany(x => x.PurchaseDetails)
            .HasForeignKey(x => x.ProductServiceId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}