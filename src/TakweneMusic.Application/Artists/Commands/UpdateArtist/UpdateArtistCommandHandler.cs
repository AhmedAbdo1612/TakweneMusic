using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Artists.Common;

namespace TakweneMusic.Application.Artists.Commands.UpdateArtist;

public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, ArtistDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ArtistDto> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.FindAsync(new object[] { request.Id }, cancellationToken);

        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with ID {request.Id} was not found.");
        }

        var emailExists = await _context.Artists.AnyAsync(
            a => a.Email.ToLower() == request.Email.ToLower() && a.Id != request.Id, cancellationToken);

        if (emailExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Email", "Email address is already in use.")
            });
        }

        artist.Update(request.Name, request.Email, request.Country);
        await _context.SaveChangesAsync(cancellationToken);

        return ArtistDto.FromEntity(artist);
    }
}
