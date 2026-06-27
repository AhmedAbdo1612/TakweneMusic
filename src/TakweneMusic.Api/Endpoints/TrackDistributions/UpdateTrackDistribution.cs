using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Commands.UpdateTrackDistribution;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public class UpdateTrackDistribution : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/track-distributions/{id:guid}", async (Guid id, UpdateTrackDistributionRequest request, ISender sender) =>
        {
            var command = new UpdateTrackDistributionCommand(id, request.TrackId, request.DspId, request.SubmittedAt, request.Status);
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Track distribution updated successfully."));
        }).RequireAuthorization();
    }
}

public record UpdateTrackDistributionRequest(Guid? TrackId, Guid DspId, DateTime SubmittedAt, DistributionStatus Status);
