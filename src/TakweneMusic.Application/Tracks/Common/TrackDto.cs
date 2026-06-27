using System;
using TakweneMusic.Domain.Entities;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Application.Tracks.Common;

public class TrackDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid ArtistId { get; set; }
    public string Isrc { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; } = string.Empty;
    public TrackStatus Status { get; set; }

    public static TrackDto FromEntity(Track track)
    {
        return new TrackDto
        {
            Id = track.Id,
            Title = track.Title,
            ArtistId = track.ArtistId,
            Isrc = track.Isrc,
            ReleaseDate = track.ReleaseDate,
            Genre = track.Genre,
            Status = track.Status
        };
    }
}
