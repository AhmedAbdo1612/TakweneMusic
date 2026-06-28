using FluentValidation;

namespace TakweneMusic.Application.Tracks.Commands.UpdateTrack;

public class UpdateTrackCommandValidator : AbstractValidator<UpdateTrackCommand>
{
    public UpdateTrackCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.ArtistId)
            .NotEmpty().WithMessage("ArtistId is required.");

        RuleFor(x => x.Isrc)
            .NotEmpty().WithMessage("ISRC is required.")
            .Matches(@"^[A-Za-z]{2}[A-Za-z0-9]{3}\d{7}$").WithMessage("Invalid ISRC format. Expected format: 2 letters, 3 alphanumeric characters, followed by 7 digits (e.g., USBM11500001).");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required.")
            .MaximumLength(50).WithMessage("Genre must not exceed 50 characters.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid track status.");
    }
}
