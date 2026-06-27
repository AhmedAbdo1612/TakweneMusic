using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token).IsRequired().HasMaxLength(256);
        builder.Property(rt => rt.Expiry).IsRequired();
        builder.Property(rt => rt.IsUsed).IsRequired();

        builder.HasIndex(rt => rt.Token).IsUnique();
    }
}
