using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("PurchaseId");
        builder.Property(x => x.Observation)
            .IsUnicode(false);
        builder.Property(x => x.SubTotal)
            .HasPrecision(10, 2);
        builder.Property(x => x.IVA)
            .HasPrecision(10, 2);
        builder.Property(x => x.Total)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Supplier)
            .WithMany(y => y.Purchases)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}