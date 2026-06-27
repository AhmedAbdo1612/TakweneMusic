using System;
using TakweneMusic.Domain.Entities;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Application.TrackDistributions.Common;

public class TrackDistributionDto
{
    public Guid Id { get; set; }
    public Guid? TrackId { get; set; }
    public Guid DspId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DistributionStatus Status { get; set; }

    public static TrackDistributionDto FromEntity(TrackDistribution trackDistribution)
    {
        return new TrackDistributionDto
        {
            Id = trackDistribution.Id,
            TrackId = trackDistribution.TrackId,
            DspId = trackDistribution.DspId,
            SubmittedAt = trackDistribution.SubmittedAt,
            Status = trackDistribution.Status
        };
    }
}
