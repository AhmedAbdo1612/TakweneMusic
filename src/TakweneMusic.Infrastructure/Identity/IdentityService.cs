using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Users.Common;
using TakweneMusic.Domain.Entities;
using TakweneMusic.Infrastructure.Persistence;

namespace TakweneMusic.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;
    private readonly JwtProvider _jwtProvider;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        AppDbContext context,
        JwtProvider jwtProvider)
    {
        _userManager = userManager;
        _context = context;
        _jwtProvider = jwtProvider;
    }

    public async Task<AuthResponseDto> RegisterAsync(string username, string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
        {
            throw new ArgumentException("A user with this email already exists.");
        }

        var user = new ApplicationUser(username, email);

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ArgumentException($"User registration failed: {errors}");
        }

        var accessToken = _jwtProvider.GenerateAccessToken(user);
        var refreshTokenValue = _jwtProvider.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);

        var refreshToken = new RefreshToken(
            Guid.NewGuid(),
            user.Id,
            refreshTokenValue,
            expiry
        );

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            Username = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty
        };
    }

    public async Task<AuthResponseDto> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var accessToken = _jwtProvider.GenerateAccessToken(user);
        var refreshTokenValue = _jwtProvider.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);

        var refreshToken = new RefreshToken(
            Guid.NewGuid(),
            user.Id,
            refreshTokenValue,
            expiry
        );

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            Username = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty
        };
    }

    public async Task<AuthResponseDto> RotateRefreshTokenAsync(string tokenValue)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == tokenValue);

        if (refreshToken == null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        if (refreshToken.IsUsed)
        {
            throw new UnauthorizedAccessException("Refresh token has already been used.");
        }

        if (refreshToken.Expiry < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token has expired.");
        }

        var user = refreshToken.User;
        if (user == null)
        {
            user = await _userManager.FindByIdAsync(refreshToken.UserId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }
        }

        refreshToken.MarkAsUsed();

        var newAccessToken = _jwtProvider.GenerateAccessToken(user);
        var newRefreshTokenValue = _jwtProvider.GenerateRefreshToken();
        var newExpiry = DateTime.UtcNow.AddDays(7);

        var newRefreshToken = new RefreshToken(
            Guid.NewGuid(),
            user.Id,
            newRefreshTokenValue,
            newExpiry
        );

        _context.RefreshTokens.Add(newRefreshToken);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshTokenValue,
            Username = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty
        };
    }
}
