using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Tracks.Common;

namespace TakweneMusic.Application.Tracks.Queries.GetTracks;

public class GetTracksQueryHandler : IRequestHandler<GetTracksQuery, List<TrackDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTracksQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrackDto>> Handle(GetTracksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tracks.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Genre))
        {
            query = query.Where(t => t.Genre.ToLower() == request.Genre.ToLower());
        }

        if (request.ArtistId.HasValue)
        {
            query = query.Where(t => t.ArtistId == request.ArtistId.Value);
        }

        var tracks = await query.ToListAsync(cancellationToken);

        return tracks.Select(TrackDto.FromEntity).ToList();
    }
}
