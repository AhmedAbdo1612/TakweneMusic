using System.Collections.Generic;
using MediatR;
using TakweneMusic.Application.TrackDistributions.Common;

namespace TakweneMusic.Application.TrackDistributions.Queries.GetTrackDistributions;

public record GetTrackDistributionsQuery : IRequest<List<TrackDistributionDto>>;
