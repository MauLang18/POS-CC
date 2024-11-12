using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;

namespace POS.Infrastructure.Persistence.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("UserId");
        builder.Property(x => x.UserName)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(x => x.Password)
            .IsUnicode(false);
    }
}