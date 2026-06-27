using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Artists.Common;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Application.Artists.Commands.CreateArtist;

public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, ArtistDto>
{
    private readonly IApplicationDbContext _context;

    public CreateArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ArtistDto> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _context.Artists.AnyAsync(
            a => a.Email.ToLower() == request.Email.ToLower(), cancellationToken);

        if (emailExists)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("Email", "Email address is already in use.")
            });
        }

        var artist = new Artist(
            Guid.NewGuid(),
            request.Name,
            request.Email,
            request.Country
        );

        _context.Artists.Add(artist);
        await _context.SaveChangesAsync(cancellationToken);

        return ArtistDto.FromEntity(artist);
    }
}
