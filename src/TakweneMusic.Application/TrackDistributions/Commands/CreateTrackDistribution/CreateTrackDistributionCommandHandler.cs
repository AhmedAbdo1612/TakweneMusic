using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.TrackDistributions.Common;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Application.TrackDistributions.Commands.CreateTrackDistribution;

public class CreateTrackDistributionCommandHandler : IRequestHandler<CreateTrackDistributionCommand, TrackDistributionDto>
{
    private readonly IApplicationDbContext _context;

    public CreateTrackDistributionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackDistributionDto> Handle(CreateTrackDistributionCommand request, CancellationToken cancellationToken)
    {
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

        var trackDistribution = new TrackDistribution(
            Guid.NewGuid(),
            request.TrackId,
            request.DspId,
            request.SubmittedAt,
            request.Status
        );

        _context.TrackDistributions.Add(trackDistribution);
        await _context.SaveChangesAsync(cancellationToken);

        return TrackDistributionDto.FromEntity(trackDistribution);
    }
}
