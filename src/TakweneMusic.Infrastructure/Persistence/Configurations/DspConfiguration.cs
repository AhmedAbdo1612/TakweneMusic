using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Infrastructure.Persistence.Configurations;

public class DspConfiguration : IEntityTypeConfiguration<Dsp>
{
    public void Configure(EntityTypeBuilder<Dsp> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);

        // Seed Data
        builder.HasData(
            new Dsp(Guid.Parse("44444444-4444-4444-4444-444444444444"), "Spotify"),
            new Dsp(Guid.Parse("55555555-5555-5555-5555-555555555555"), "Apple Music"),
            new Dsp(Guid.Parse("66666666-6666-6666-6666-666666666666"), "YouTube Music")
        );
    }
}
