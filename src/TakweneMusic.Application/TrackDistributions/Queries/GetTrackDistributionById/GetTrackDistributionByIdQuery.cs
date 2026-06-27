using System;
using MediatR;
using TakweneMusic.Application.TrackDistributions.Common;

namespace TakweneMusic.Application.TrackDistributions.Queries.GetTrackDistributionById;

public record GetTrackDistributionByIdQuery(Guid Id) : IRequest<TrackDistributionDto>;
