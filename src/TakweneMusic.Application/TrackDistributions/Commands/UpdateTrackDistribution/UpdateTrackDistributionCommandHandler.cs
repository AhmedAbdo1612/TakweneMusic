using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.TrackDistributions.Common;

namespace TakweneMusic.Application.TrackDistributions.Commands.UpdateTrackDistribution;

public class UpdateTrackDistributionCommandHandler : IRequestHandler<UpdateTrackDistributionCommand, TrackDistributionDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateTrackDistributionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackDistributionDto> Handle(UpdateTrackDistributionCommand request, CancellationToken cancellationToken)
    {
        var trackDistribution = await _context.TrackDistributions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (trackDistribution == null)
        {
            throw new KeyNotFoundException($"TrackDistribution with ID {request.Id} was not found.");
        }

        if (request.TrackId.HasValue)
        {
            var trackExists = await _context.Tracks.AnyAsync(t => t.Id == request.TrackId.Value, cancellationToken);
            if (!trackExists)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("TrackId", "The specified Track does not exist.")
                });
            }
        }

        var dspExists = await _context.Dsps.AnyAsync(d => d.Id == request.DspId, cancellationToken);
        if (!dspExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("DspId", "The specified DSP does not exist.")
            });
        }

        trackDistribution.Update(request.TrackId, request.DspId, request.SubmittedAt, request.Status);
        await _context.SaveChangesAsync(cancellationToken);

        return TrackDistributionDto.FromEntity(trackDistribution);
    }
}
