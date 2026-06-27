using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Application.Dsps.Queries.GetDspById;

public class GetDspByIdQueryHandler : IRequestHandler<GetDspByIdQuery, DspDto>
{
    private readonly IApplicationDbContext _context;

    public GetDspByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DspDto> Handle(GetDspByIdQuery request, CancellationToken cancellationToken)
    {
        var dsp = await _context.Dsps.AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (dsp == null)
        {
            throw new KeyNotFoundException($"DSP with ID {request.Id} was not found.");
        }

        return DspDto.FromEntity(dsp);
    }
}
