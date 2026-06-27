using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Dsps.Commands.DeleteDsp;

public class DeleteDspCommandHandler : IRequestHandler<DeleteDspCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteDspCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDspCommand request, CancellationToken cancellationToken)
    {
        var dsp = await _context.Dsps.FindAsync(new object[] { request.Id }, cancellationToken);

        if (dsp == null)
        {
            throw new KeyNotFoundException($"DSP with ID {request.Id} was not found.");
        }

        _context.Dsps.Remove(dsp);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
