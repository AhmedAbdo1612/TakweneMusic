using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.TrackDistributions.Common;

namespace TakweneMusic.Application.TrackDistributions.Queries.GetTrackDistributions;

public class GetTrackDistributionsQueryHandler : IRequestHandler<GetTrackDistributionsQuery, List<TrackDistributionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTrackDistributionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrackDistributionDto>> Handle(GetTrackDistributionsQuery request, CancellationToken cancellationToken)
    {
        var trackDistributions = await _context.TrackDistributions.AsNoTracking()
            .Select(td => TrackDistributionDto.FromEntity(td))
            .ToListAsync(cancellationToken);

        return trackDistributions;
    }
}
