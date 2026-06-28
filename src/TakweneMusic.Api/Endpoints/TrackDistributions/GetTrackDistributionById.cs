using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Common;
using TakweneMusic.Application.TrackDistributions.Queries.GetTrackDistributionById;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public static class GetTrackDistributionById
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetTrackDistributionByIdQuery(id));
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetTrackDistributionById")
        .WithSummary("Retrieve a track distribution by ID")
        .WithDescription("Fetches a specific track distribution workflow details by its unique identifier.")
        .Produces<ApiResponse<TrackDistributionDto>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
