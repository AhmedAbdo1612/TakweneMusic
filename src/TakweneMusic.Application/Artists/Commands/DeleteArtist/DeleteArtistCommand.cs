using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Artists.Commands.DeleteArtist;

public record DeleteArtistCommand(Guid Id) : ICommand<bool>;
