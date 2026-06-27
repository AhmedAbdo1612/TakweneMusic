using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : ICommand<AuthResponseDto>;
