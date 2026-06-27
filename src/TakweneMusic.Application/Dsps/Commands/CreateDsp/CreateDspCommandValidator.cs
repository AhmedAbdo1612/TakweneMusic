using FluentValidation;

namespace TakweneMusic.Application.Dsps.Commands.CreateDsp;

public class CreateDspCommandValidator : AbstractValidator<CreateDspCommand>
{
    public CreateDspCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}
