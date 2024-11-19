using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
{
    public void Configure(EntityTypeBuilder<SaleDetail> builder)
    {
        builder.HasKey(x => new { x.SaleId, x.ProductServiceId });
        builder.Property(x => x.Price)
            .HasPrecision(10, 2);
        builder.Property(x => x.Total)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Sale)
            .WithMany(y => y.SaleDetails)
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.ProductService)
            .WithMany(y => y.SaleDetails)
            .HasForeignKey(x => x.ProductServiceId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}