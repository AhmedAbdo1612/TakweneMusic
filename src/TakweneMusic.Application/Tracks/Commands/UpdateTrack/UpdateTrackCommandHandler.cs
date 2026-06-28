using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Tracks.Common;

namespace TakweneMusic.Application.Tracks.Commands.UpdateTrack;

public class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand, TrackDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackDto> Handle(UpdateTrackCommand request, CancellationToken cancellationToken)
    {
        var track = await _context.Tracks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (track == null)
        {
            throw new KeyNotFoundException($"Track with ID {request.Id} was not found.");
        }

        var artistExists = await _context.Artists.AnyAsync(a => a.Id == request.ArtistId, cancellationToken);
        if (!artistExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("ArtistId", "Artist does not exist.")
            });
        }

        var isrcExists = await _context.Tracks.AnyAsync(
            t => t.Isrc.ToLower() == request.Isrc.ToLower() && t.Id != request.Id, cancellationToken);
        if (isrcExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Isrc", "ISRC must be unique.")
            });
        }

        track.Update(
            request.Title,
            request.ArtistId,
            request.Isrc.ToUpperInvariant(),
            request.ReleaseDate,
            request.Genre,
            request.Status
        );

        await _context.SaveChangesAsync(cancellationToken);

        return TrackDto.FromEntity(track);
    }
}
