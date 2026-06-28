using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Tracks.Commands.DeleteTrack;

public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteTrackCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTrackCommand request, CancellationToken cancellationToken)
    {
        var track = await _context.Tracks.FindAsync(new object[] { request.Id }, cancellationToken);

        if (track == null)
        {
            throw new KeyNotFoundException($"Track with ID {request.Id} was not found.");
        }

        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
