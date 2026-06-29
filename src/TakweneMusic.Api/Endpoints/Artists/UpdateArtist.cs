using System;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Commands.UpdateArtist;
using TakweneMusic.Application.Artists.Common;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public static class UpdateArtist
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapPut("/{id:guid}", async (Guid id, UpdateArtistRequest request, ISender sender) =>
        {
            var command = new UpdateArtistCommand(id, request.Name, request.Email, request.Country);
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Artist updated successfully."));
        })
        .WithName("UpdateArtist")
        .WithSummary("Update an artist")
        .WithDescription("Updates the profile details of an existing artist by their ID.")
        .Produces<ApiResponse<ArtistDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}

public record UpdateArtistRequest(string Name, string Email, string Country);
