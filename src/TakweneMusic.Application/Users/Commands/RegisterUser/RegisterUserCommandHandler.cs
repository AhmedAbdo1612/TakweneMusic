using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RegisterAsync(request.FullName, request.Email, request.Password);
    }
}
