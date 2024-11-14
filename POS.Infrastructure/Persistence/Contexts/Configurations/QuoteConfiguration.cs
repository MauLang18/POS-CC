using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("QuoteId");
        builder.Property(x => x.VoucherNumber)
            .HasMaxLength(10)
            .IsUnicode(false);
        builder.Property(x => x.Observation)
            .IsUnicode(false);
        builder.Property(x => x.SubTotal)
            .HasPrecision(10, 2);
        builder.Property(x => x.IVA)
            .HasPrecision(10, 2);
        builder.Property(x => x.Discount)
            .HasPrecision(10, 2);
        builder.Property(x => x.Total)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Customer)
            .WithMany(y => y.Quotes)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.VoucherType)
            .WithMany(y => y.Quotes)
            .HasForeignKey(x => x.VoucherTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Status)
            .WithMany(y => y.Quotes)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.PaymentMethod)
            .WithMany(y => y.Quotes)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}