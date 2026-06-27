using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.TrackDistributions.Commands.DeleteTrackDistribution;

public record DeleteTrackDistributionCommand(Guid Id) : ICommand<bool>;
