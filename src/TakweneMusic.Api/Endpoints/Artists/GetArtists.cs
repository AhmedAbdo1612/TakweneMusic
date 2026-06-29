using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Common;
using TakweneMusic.Application.Artists.Queries.GetArtists;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public static class GetArtists
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet("", async (ISender sender) =>
        {
            var result = await sender.Send(new GetArtistsQuery());
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetArtists")
        .WithSummary("Retrieve all artists")
        .WithDescription("Fetches a list of all artists in the system.")
        .Produces<ApiResponse<List<ArtistDto>>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
