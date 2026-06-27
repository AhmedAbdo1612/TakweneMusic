using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Application.Dsps.Commands.UpdateDsp;

public class UpdateDspCommandHandler : IRequestHandler<UpdateDspCommand, DspDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateDspCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DspDto> Handle(UpdateDspCommand request, CancellationToken cancellationToken)
    {
        var dsp = await _context.Dsps.FindAsync(new object[] { request.Id }, cancellationToken);

        if (dsp == null)
        {
            throw new KeyNotFoundException($"DSP with ID {request.Id} was not found.");
        }

        var dspExists = await _context.Dsps.AnyAsync(
            d => d.Name.ToLower() == request.Name.ToLower() && d.Id != request.Id, cancellationToken);

        if (dspExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Name", "DSP name already exists.")
            });
        }

        dsp.Update(request.Name);
        await _context.SaveChangesAsync(cancellationToken);

        return DspDto.FromEntity(dsp);
    }
}
