using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Artists.Common;

namespace TakweneMusic.Application.Artists.Queries.GetArtistById;

public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistDto>
{
    private readonly IApplicationDbContext _context;

    public GetArtistByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ArtistDto> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with ID {request.Id} was not found.");
        }

        return ArtistDto.FromEntity(artist);
    }
}
