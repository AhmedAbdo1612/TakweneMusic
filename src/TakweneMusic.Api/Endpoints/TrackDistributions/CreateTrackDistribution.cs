using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Commands.CreateTrackDistribution;
using TakweneMusic.Application.TrackDistributions.Common;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public static class CreateTrackDistribution
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapPost("", async (CreateTrackDistributionCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Track distribution created successfully."));
        })
        .WithName("CreateTrackDistribution")
        .WithSummary("Create a new track distribution")
        .WithDescription("Creates a new track distribution task mapping a track to a DSP platform.")
        .Produces<ApiResponse<TrackDistributionDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
