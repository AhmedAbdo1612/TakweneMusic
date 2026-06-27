using System.Collections.Generic;
using MediatR;
using TakweneMusic.Application.Artists.Common;

namespace TakweneMusic.Application.Artists.Queries.GetArtists;

public record GetArtistsQuery : IRequest<List<ArtistDto>>;
