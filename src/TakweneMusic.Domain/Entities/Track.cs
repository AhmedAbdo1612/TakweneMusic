using System;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Domain.Entities;

public class Track
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public Guid ArtistId { get; private set; }
    public string Isrc { get; private set; } = string.Empty;
    public DateTime ReleaseDate { get; private set; }
    public string Genre { get; private set; } = string.Empty;
    public TrackStatus Status { get; private set; }

    // Parameterless constructor for serialization / EF Core compatibility
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Track() { }
#pragma warning restore CS8618

    public Track(Guid id, string title, Guid artistId, string isrc, DateTime releaseDate, string genre, TrackStatus status)
    {
        Id = id;
        Title = title;
        ArtistId = artistId;
        Isrc = isrc;
        ReleaseDate = releaseDate;
        Genre = genre;
        Status = status;
    }
}
