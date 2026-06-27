using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.TrackDistributions.Common;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Application.TrackDistributions.Commands.UpdateTrackDistribution;

public record UpdateTrackDistributionCommand(Guid Id, Guid? TrackId, Guid DspId, DateTime SubmittedAt, DistributionStatus Status) : ICommand<TrackDistributionDto>;
