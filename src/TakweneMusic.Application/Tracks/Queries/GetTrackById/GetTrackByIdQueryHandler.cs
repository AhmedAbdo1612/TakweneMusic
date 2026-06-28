using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Tracks.Common;

namespace TakweneMusic.Application.Tracks.Queries.GetTrackById;

public class GetTrackByIdQueryHandler : IRequestHandler<GetTrackByIdQuery, TrackDto>
{
    private readonly IApplicationDbContext _context;

    public GetTrackByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackDto> Handle(GetTrackByIdQuery request, CancellationToken cancellationToken)
    {
        var track = await _context.Tracks.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (track == null)
        {
            throw new KeyNotFoundException($"Track with ID {request.Id} was not found.");
        }

        return TrackDto.FromEntity(track);
    }
}
