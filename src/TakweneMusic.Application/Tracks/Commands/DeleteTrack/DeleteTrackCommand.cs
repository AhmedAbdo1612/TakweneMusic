using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Tracks.Commands.DeleteTrack;

public record DeleteTrackCommand(Guid Id) : ICommand<bool>;
