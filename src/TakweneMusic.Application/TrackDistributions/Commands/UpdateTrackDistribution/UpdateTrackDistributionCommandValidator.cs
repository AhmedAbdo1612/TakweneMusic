using FluentValidation;

namespace TakweneMusic.Application.TrackDistributions.Commands.UpdateTrackDistribution;

public class UpdateTrackDistributionCommandValidator : AbstractValidator<UpdateTrackDistributionCommand>
{
    public UpdateTrackDistributionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.DspId)
            .NotEmpty().WithMessage("DSP ID is required.");

        RuleFor(x => x.SubmittedAt)
            .NotEmpty().WithMessage("Submitted date is required.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("A valid distribution status is required.");
    }
}
