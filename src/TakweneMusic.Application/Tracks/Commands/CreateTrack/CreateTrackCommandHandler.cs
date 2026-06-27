using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Tracks.Common;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Application.Tracks.Commands.CreateTrack;

public class CreateTrackCommandHandler : IRequestHandler<CreateTrackCommand, TrackDto>
{
    private readonly IApplicationDbContext _context;

    public CreateTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackDto> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
    {
        var artistExists = await _context.Artists.AnyAsync(a => a.Id == request.ArtistId, cancellationToken);
        if (!artistExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("ArtistId", "Artist does not exist.")
            });
        }

        var isrcExists = await _context.Tracks.AnyAsync(
            t => t.Isrc.ToLower() == request.Isrc.ToLower(), cancellationToken);
        if (isrcExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Isrc", "ISRC must be unique.")
            });
        }

        var track = new Track(
            Guid.NewGuid(),
            request.Title,
            request.ArtistId,
            request.Isrc.ToUpperInvariant(),
            request.ReleaseDate,
            request.Genre,
            request.Status
        );

        await _context.Tracks.AddAsync(track, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return TrackDto.FromEntity(track);
    }
}
