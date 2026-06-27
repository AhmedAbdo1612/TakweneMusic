using FluentValidation;

namespace TakweneMusic.Application.Dsps.Commands.UpdateDsp;

public class UpdateDspCommandValidator : AbstractValidator<UpdateDspCommand>
{
    public UpdateDspCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}
