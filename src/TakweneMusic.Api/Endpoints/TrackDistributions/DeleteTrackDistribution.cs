using System;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Commands.DeleteTrackDistribution;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public static class DeleteTrackDistribution
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTrackDistributionCommand(id));
            return Results.Ok(ApiResponse.Success(result, "Track distribution deleted successfully."));
        })
        .WithName("DeleteTrackDistribution")
        .WithSummary("Delete a track distribution")
        .WithDescription("Deletes a track distribution task from the system by its ID.")
        .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
