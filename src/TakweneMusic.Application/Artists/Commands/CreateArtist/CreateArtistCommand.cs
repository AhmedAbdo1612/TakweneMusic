using MediatR;
using TakweneMusic.Application.Artists.Common;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Artists.Commands.CreateArtist;

public record CreateArtistCommand(string Name, string Email, string Country) : ICommand<ArtistDto>;
