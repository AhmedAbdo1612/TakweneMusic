using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Application.Dsps.Queries.GetDsps;

public class GetDspsQueryHandler : IRequestHandler<GetDspsQuery, List<DspDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDspsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DspDto>> Handle(GetDspsQuery request, CancellationToken cancellationToken)
    {
        var dsps = await _context.Dsps.AsNoTracking()
            .Select(d => DspDto.FromEntity(d))
            .ToListAsync(cancellationToken);

        return dsps;
    }
}
