using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.TrackDistributions.Commands.DeleteTrackDistribution;

public class DeleteTrackDistributionCommandHandler : IRequestHandler<DeleteTrackDistributionCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteTrackDistributionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTrackDistributionCommand request, CancellationToken cancellationToken)
    {
        var trackDistribution = await _context.TrackDistributions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (trackDistribution == null)
        {
            throw new KeyNotFoundException($"TrackDistribution with ID {request.Id} was not found.");
        }

        _context.TrackDistributions.Remove(trackDistribution);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
