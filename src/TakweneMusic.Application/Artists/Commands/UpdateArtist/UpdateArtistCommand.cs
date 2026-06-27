using System;
using MediatR;
using TakweneMusic.Application.Artists.Common;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Artists.Commands.UpdateArtist;

public record UpdateArtistCommand(Guid Id, string Name, string Email, string Country) : ICommand<ArtistDto>;
