using System;
using System.Collections.Generic;
using MediatR;
using TakweneMusic.Application.Tracks.Common;

namespace TakweneMusic.Application.Tracks.Queries.GetTracks;

public record GetTracksQuery(
    string? Genre = null,
    Guid? ArtistId = null
) : IRequest<List<TrackDto>>;
