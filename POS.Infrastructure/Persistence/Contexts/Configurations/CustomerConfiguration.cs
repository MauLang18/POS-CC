using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("CustomerId");
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(x => x.DocumentNumber)
            .HasMaxLength(15)
            .IsUnicode(false);
        builder.Property(x => x.Address)
            .IsUnicode(false);
        builder.Property(x => x.Phone)
            .HasMaxLength(25)
            .IsUnicode(false);
        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(x => x.DiscountPercent)
            .HasPrecision(5, 2);
        builder.Property(x => x.CreditInterestRate)
            .HasPrecision(5, 2);
        builder.Property(x => x.CreditLimit)
            .HasPrecision(12, 2);
        builder.HasOne(x => x.DocumentType)
            .WithMany(y => y.Customers)
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.CreditType)
            .WithMany(y => y.Customers)
            .HasForeignKey(x => x.CreditTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}