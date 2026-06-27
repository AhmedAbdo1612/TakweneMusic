using System;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Domain.Entities;

public class TrackDistribution
{
    public Guid Id { get; private set; }
    public Guid? TrackId { get; private set; }
    public Guid DspId { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public DistributionStatus Status { get; private set; }

    // Parameterless constructor for serialization / EF Core compatibility
    private TrackDistribution() { }

    public TrackDistribution(Guid id, Guid? trackId, Guid dspId, DateTime submittedAt, DistributionStatus status)
    {
        Id = id;
        TrackId = trackId;
        DspId = dspId;
        SubmittedAt = submittedAt;
        Status = status;
    }

    public void Update(Guid? trackId, Guid dspId, DateTime submittedAt, DistributionStatus status)
    {
        TrackId = trackId;
        DspId = dspId;
        SubmittedAt = submittedAt;
        Status = status;
    }
}
