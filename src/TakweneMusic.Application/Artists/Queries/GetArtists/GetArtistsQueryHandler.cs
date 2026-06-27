using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Artists.Common;

namespace TakweneMusic.Application.Artists.Queries.GetArtists;

public class GetArtistsQueryHandler : IRequestHandler<GetArtistsQuery, List<ArtistDto>>
{
    private readonly IApplicationDbContext _context;

    public GetArtistsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArtistDto>> Handle(GetArtistsQuery request, CancellationToken cancellationToken)
    {
        var artists = await _context.Artists.AsNoTracking()
            .Select(a => ArtistDto.FromEntity(a))
            .ToListAsync(cancellationToken);

        return artists;
    }
}
