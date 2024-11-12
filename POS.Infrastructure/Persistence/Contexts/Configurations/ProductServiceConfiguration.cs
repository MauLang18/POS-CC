using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class ProductServiceConfiguration : IEntityTypeConfiguration<ProductService>
{
    public void Configure(EntityTypeBuilder<ProductService> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("ProductServiceId");
        builder.Property(x => x.Code)
            .HasMaxLength(7)
            .IsUnicode(false);
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(x => x.Image)
            .IsUnicode(false);
        builder.Property(x => x.Description)
            .IsUnicode(false);
        builder.Property(x => x.Price)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Unit)
            .WithMany(y => y.ProductServices)
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Category)
            .WithMany(y => y.ProductServices)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}