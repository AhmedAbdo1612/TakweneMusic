using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Tracks.Common;
using TakweneMusic.Application.Tracks.Queries.GetTracks;

namespace TakweneMusic.Api.Endpoints.Tracks;

public static class GetTracks
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet("", async (
            [FromQuery] string? genre,
            [FromQuery] Guid? artistId,
            ISender sender) =>
        {
            var result = await sender.Send(new GetTracksQuery(genre, artistId));
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetTracks")
        .WithSummary("Retrieve tracks")
        .WithDescription("Queries and filters musical tracks by optional genre or artist identifiers.")
        .Produces<ApiResponse<List<TrackDto>>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
