using FluentValidation;

namespace TakweneMusic.Application.Users.Commands.RotateRefreshToken;

public class RotateRefreshTokenCommandValidator : AbstractValidator<RotateRefreshTokenCommand>
{
    public RotateRefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}
