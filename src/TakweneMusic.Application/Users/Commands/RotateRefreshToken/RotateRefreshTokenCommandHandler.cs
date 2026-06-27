using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Application.Users.Commands.RotateRefreshToken;

public class RotateRefreshTokenCommandHandler : IRequestHandler<RotateRefreshTokenCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;

    public RotateRefreshTokenCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthResponseDto> Handle(RotateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RotateRefreshTokenAsync(request.RefreshToken);
    }
}
