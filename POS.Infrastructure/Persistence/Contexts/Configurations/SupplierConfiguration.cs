using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("SupplierId");
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(x => x.ContactName)
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
        builder.HasOne(x => x.DocumentType)
            .WithMany(y => y.Suppliers)
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}