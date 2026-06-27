using System;

namespace TakweneMusic.Domain.Entities;

public class Artist
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    // Parameterless constructor for serialization / EF Core compatibility
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Artist() { }
#pragma warning restore CS8618

    public Artist(Guid id, string name, string email, string country)
    {
        Id = id;
        Name = name;
        Email = email;
        Country = country;
    }

    public void Update(string name, string email, string country)
    {
        Name = name;
        Email = email;
        Country = country;
    }
}
