using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakweneMusic.Domain.Entities;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Infrastructure.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Isrc).IsRequired().HasMaxLength(20);
        builder.Property(t => t.Genre).IsRequired().HasMaxLength(50);
        builder.Property(t => t.Status).HasConversion<string>();

        builder.HasIndex(t => t.Isrc).IsUnique();

        builder.HasOne<Artist>()
               .WithMany()
               .HasForeignKey(t => t.ArtistId)
               .OnDelete(DeleteBehavior.Restrict);

        // Seed Data
        builder.HasData(
            new Track(Guid.Parse("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "Hello", Guid.Parse("11111111-1111-1111-1111-111111111111"), "USBM11500001", DateTime.SpecifyKind(new DateTime(2015, 10, 23), DateTimeKind.Utc), "Pop", TrackStatus.Distributed),
            new Track(Guid.Parse("aaaaaaaa-2222-2222-2222-aaaaaaaaaaaa"), "Easy On Me", Guid.Parse("11111111-1111-1111-1111-111111111111"), "USBM12100002", DateTime.SpecifyKind(new DateTime(2021, 10, 15), DateTimeKind.Utc), "Pop", TrackStatus.Distributed),
            new Track(Guid.Parse("aaaaaaaa-3333-3333-3333-aaaaaaaaaaaa"), "New Track Demo", Guid.Parse("11111111-1111-1111-1111-111111111111"), "USBM12600003", DateTime.SpecifyKind(new DateTime(2026, 6, 1), DateTimeKind.Utc), "Pop", TrackStatus.Draft),
            new Track(Guid.Parse("bbbbbbbb-1111-1111-1111-bbbbbbbbbbbb"), "Blinding Lights", Guid.Parse("22222222-2222-2222-2222-222222222222"), "USUM71900004", DateTime.SpecifyKind(new DateTime(2019, 11, 29), DateTimeKind.Utc), "Synth-wave", TrackStatus.Distributed),
            new Track(Guid.Parse("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Save Your Tears", Guid.Parse("22222222-2222-2222-2222-222222222222"), "USUM72000005", DateTime.SpecifyKind(new DateTime(2020, 8, 9), DateTimeKind.Utc), "Synth-pop", TrackStatus.Distributed),
            new Track(Guid.Parse("bbbbbbbb-3333-3333-3333-bbbbbbbbbbbb"), "Submitted Single", Guid.Parse("22222222-2222-2222-2222-222222222222"), "USUM72600006", DateTime.SpecifyKind(new DateTime(2026, 7, 1), DateTimeKind.Utc), "R&B", TrackStatus.Submitted),
            new Track(Guid.Parse("cccccccc-1111-1111-1111-cccccccccccc"), "Yellow", Guid.Parse("33333333-3333-3333-3333-333333333333"), "GBAYE0000007", DateTime.SpecifyKind(new DateTime(2000, 6, 26), DateTimeKind.Utc), "Alternative Rock", TrackStatus.Distributed),
            new Track(Guid.Parse("cccccccc-2222-2222-2222-cccccccccccc"), "Viva la Vida", Guid.Parse("33333333-3333-3333-3333-333333333333"), "GBAYE0800008", DateTime.SpecifyKind(new DateTime(2008, 5, 25), DateTimeKind.Utc), "Alternative Rock", TrackStatus.Distributed)
        );
    }
}
