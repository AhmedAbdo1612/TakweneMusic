using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Artists.Commands.DeleteArtist;

public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.FindAsync(new object[] { request.Id }, cancellationToken);

        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with ID {request.Id} was not found.");
        }

        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
