using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.TrackDistributions.Common;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Application.TrackDistributions.Commands.CreateTrackDistribution;

public record CreateTrackDistributionCommand(Guid? TrackId, Guid DspId, DateTime SubmittedAt, DistributionStatus Status) : ICommand<TrackDistributionDto>;
