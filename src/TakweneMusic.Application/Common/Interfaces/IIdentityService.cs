using System.Threading.Tasks;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<AuthResponseDto> RegisterAsync(string username, string email, string password);
    Task<AuthResponseDto> LoginAsync(string email, string password);
    Task<AuthResponseDto> RotateRefreshTokenAsync(string refreshToken);
}
