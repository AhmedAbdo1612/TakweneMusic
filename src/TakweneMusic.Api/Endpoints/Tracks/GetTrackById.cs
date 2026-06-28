using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Tracks.Common;
using TakweneMusic.Application.Tracks.Queries.GetTrackById;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Tracks;

public static class GetTrackById
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetTrackByIdQuery(id));
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetTrackById")
        .WithSummary("Retrieve a track by ID")
        .WithDescription("Fetches detailed track information for a specific track by its unique identifier.")
        .Produces<ApiResponse<TrackDto>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
