using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Username, string Email, string Password) : ICommand<AuthResponseDto>;
