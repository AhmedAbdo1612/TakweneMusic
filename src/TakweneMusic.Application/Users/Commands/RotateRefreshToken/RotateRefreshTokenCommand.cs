using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Application.Users.Commands.RotateRefreshToken;

public record RotateRefreshTokenCommand(string RefreshToken) : ICommand<AuthResponseDto>;
