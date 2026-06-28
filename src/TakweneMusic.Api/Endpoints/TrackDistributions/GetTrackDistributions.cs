using System.Collections.Generic;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Common;
using TakweneMusic.Application.TrackDistributions.Queries.GetTrackDistributions;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public static class GetTrackDistributions
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet("", async (ISender sender) =>
        {
            var result = await sender.Send(new GetTrackDistributionsQuery());
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetTrackDistributions")
        .WithSummary("Retrieve all track distributions")
        .WithDescription("Fetches a list of all track distribution workflows registered in the system.")
        .Produces<ApiResponse<List<TrackDistributionDto>>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
