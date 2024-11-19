using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class QuoteDetailConfiguration : IEntityTypeConfiguration<QuoteDetail>
{
    public void Configure(EntityTypeBuilder<QuoteDetail> builder)
    {
        builder.HasKey(x => new { x.QuoteId, x.ProductServiceId });
        builder.Property(x => x.Price)
            .HasPrecision(10, 2);
        builder.Property(x => x.Total)
            .HasPrecision(10, 2);
        builder.HasOne(x => x.Quote)
            .WithMany(y => y.QuoteDetails)
            .HasForeignKey(x => x.QuoteId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.ProductService)
            .WithMany(x => x.QuoteDetails)
            .HasForeignKey(x => x.ProductServiceId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}