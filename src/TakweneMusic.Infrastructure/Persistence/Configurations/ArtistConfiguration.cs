using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Infrastructure.Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Email).IsRequired().HasMaxLength(150);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(50);

        // Seed Data
        builder.HasData(
            new Artist(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Adele", "adele@example.com", "UK"),
            new Artist(Guid.Parse("22222222-2222-2222-2222-222222222222"), "The Weeknd", "weeknd@example.com", "Canada"),
            new Artist(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Coldplay", "coldplay@example.com", "UK")
        );
    }
}
