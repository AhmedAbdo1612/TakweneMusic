using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Commands.UpdateTrackDistribution;
using TakweneMusic.Application.TrackDistributions.Common;
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
        })
        .WithName("UpdateTrackDistribution")
        .WithSummary("Update a track distribution")
        .WithDescription("Updates the parameters or submission status of an existing track distribution task by its ID.")
        .Produces<ApiResponse<TrackDistributionDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}

public record UpdateTrackDistributionRequest(Guid? TrackId, Guid DspId, DateTime SubmittedAt, DistributionStatus Status);
