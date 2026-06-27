using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.TrackDistributions.Common;

namespace TakweneMusic.Application.TrackDistributions.Queries.GetTrackDistributionById;

public class GetTrackDistributionByIdQueryHandler : IRequestHandler<GetTrackDistributionByIdQuery, TrackDistributionDto>
{
    private readonly IApplicationDbContext _context;

    public GetTrackDistributionByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TrackDistributionDto> Handle(GetTrackDistributionByIdQuery request, CancellationToken cancellationToken)
    {
        var trackDistribution = await _context.TrackDistributions.AsNoTracking()
            .FirstOrDefaultAsync(td => td.Id == request.Id, cancellationToken);

        if (trackDistribution == null)
        {
            throw new KeyNotFoundException($"TrackDistribution with ID {request.Id} was not found.");
        }

        return TrackDistributionDto.FromEntity(trackDistribution);
    }
}
