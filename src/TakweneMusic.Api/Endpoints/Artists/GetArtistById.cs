using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Common;
using TakweneMusic.Application.Artists.Queries.GetArtistById;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public class GetArtistById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/artists/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetArtistByIdQuery(id));
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetArtistById")
        .WithSummary("Retrieve an artist by ID")
        .WithDescription("Fetches detailed profile information for a specific artist by their unique identifier.")
        .Produces<ApiResponse<ArtistDto>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
