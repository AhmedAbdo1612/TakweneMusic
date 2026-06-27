using System;
using MediatR;
using TakweneMusic.Application.Artists.Common;

namespace TakweneMusic.Application.Artists.Queries.GetArtistById;

public record GetArtistByIdQuery(Guid Id) : IRequest<ArtistDto>;
