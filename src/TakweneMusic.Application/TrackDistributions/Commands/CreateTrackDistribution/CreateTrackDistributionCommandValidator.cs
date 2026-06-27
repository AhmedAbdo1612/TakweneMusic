using FluentValidation;

namespace TakweneMusic.Application.TrackDistributions.Commands.CreateTrackDistribution;

public class CreateTrackDistributionCommandValidator : AbstractValidator<CreateTrackDistributionCommand>
{
    public CreateTrackDistributionCommandValidator()
    {
        RuleFor(x => x.DspId)
            .NotEmpty().WithMessage("DSP ID is required.");

        RuleFor(x => x.SubmittedAt)
            .NotEmpty().WithMessage("Submitted date is required.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("A valid distribution status is required.");
    }
}
