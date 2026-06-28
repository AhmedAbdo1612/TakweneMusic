using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Tracks.Common;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Application.Tracks.Commands.UpdateTrack;

public record UpdateTrackCommand(
    Guid Id,
    string Title,
    Guid ArtistId,
    string Isrc,
    DateTime ReleaseDate,
    string Genre,
    TrackStatus Status
) : ICommand<TrackDto>;
