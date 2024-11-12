using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("SaleId");
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
        builder.HasOne(x => x.Customer)
            .WithMany(y => y.Sales)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Quote)
            .WithMany(y => y.Sales)
            .HasForeignKey(x => x.QuoteId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.VoucherType)
            .WithMany(y => y.Sales)
            .HasForeignKey(x => x.VoucherTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Status)
            .WithMany(y => y.Sales)
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Project)
            .WithMany(y => y.Sales)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}