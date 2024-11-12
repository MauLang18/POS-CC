using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("InvoceId");
        builder.Property(x => x.VoucherNumber)
            .HasMaxLength(10)
            .IsUnicode(false);
        builder.Property(x => x.Observation)
            .IsUnicode(false);
        builder.Property(x => x.SubTotal)
            .HasPrecision(10, 2);
        builder.Property(x => x.IVA)
            .HasPrecision(10, 2);
        builder.Property(x => x.Total)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Sale)
            .WithMany(y => y.Invoices)
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.VoucherType)
            .WithMany(y => y.Invoices)
            .HasForeignKey(x => x.VoucherTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}