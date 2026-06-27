using System;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Application.Artists.Common;

public class ArtistDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public static ArtistDto FromEntity(Artist artist)
    {
        return new ArtistDto
        {
            Id = artist.Id,
            Name = artist.Name,
            Email = artist.Email,
            Country = artist.Country
        };
    }
}
