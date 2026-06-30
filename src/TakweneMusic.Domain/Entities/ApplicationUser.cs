using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TakweneMusic.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; }    
    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    // Parameterless constructor for EF Core and Identity compatibility
    public ApplicationUser() { }

    public ApplicationUser(string fullname, string email)
    {
        Id = Guid.NewGuid();
        FullName = fullname;
        Email = email;
        UserName = email;
    }

    public void AddRefreshToken(RefreshToken token)
    {
        _refreshTokens.Add(token);
    }
}
