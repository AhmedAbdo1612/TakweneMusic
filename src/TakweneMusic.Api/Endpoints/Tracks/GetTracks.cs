using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Tracks.Queries.GetTracks;

namespace TakweneMusic.Api.Endpoints.Tracks;

public class GetTracks : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tracks", async (
            [FromQuery] string? genre,
            [FromQuery] Guid? artistId,
            ISender sender) =>
        {
            var result = await sender.Send(new GetTracksQuery(genre, artistId));
            return Results.Ok(ApiResponse.Success(result));
        }).RequireAuthorization();
    }
}
