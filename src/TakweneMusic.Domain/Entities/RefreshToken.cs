using System;

namespace TakweneMusic.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public DateTime Expiry { get; private set; }
    public bool IsUsed { get; private set; }

    // Navigation property
#pragma warning disable CS8618
    public ApplicationUser User { get; private set; } = null!;
#pragma warning restore CS8618

    // Parameterless constructor for serialization / EF Core compatibility
    private RefreshToken() { }

    public RefreshToken(Guid id, Guid userId, string token, DateTime expiry)
    {
        Id = id;
        UserId = userId;
        Token = token;
        Expiry = expiry;
        IsUsed = false;
    }

    public void MarkAsUsed()
    {
        IsUsed = true;
    }
}
