using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Dsps.Common;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Application.Dsps.Commands.CreateDsp;

public class CreateDspCommandHandler : IRequestHandler<CreateDspCommand, DspDto>
{
    private readonly IApplicationDbContext _context;

    public CreateDspCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DspDto> Handle(CreateDspCommand request, CancellationToken cancellationToken)
    {
        var dspExists = await _context.Dsps.AnyAsync(
            d => d.Name.ToLower() == request.Name.ToLower(), cancellationToken);

        if (dspExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Name", "DSP name already exists.")
            });
        }

        var dsp = new Dsp(
            Guid.NewGuid(),
            request.Name
        );

        _context.Dsps.Add(dsp);
        await _context.SaveChangesAsync(cancellationToken);

        return DspDto.FromEntity(dsp);
    }
}
