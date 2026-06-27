using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(request.Email, request.Password);
    }
}
