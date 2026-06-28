using System;
using MediatR;
using TakweneMusic.Application.Tracks.Common;

namespace TakweneMusic.Application.Tracks.Queries.GetTrackById;

public record GetTrackByIdQuery(Guid Id) : IRequest<TrackDto>;
