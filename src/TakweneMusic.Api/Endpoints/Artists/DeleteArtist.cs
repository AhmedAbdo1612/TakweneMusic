using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Commands.DeleteArtist;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public class DeleteArtist : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/artists/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteArtistCommand(id));
            return Results.Ok(ApiResponse.Success(result, "Artist deleted successfully."));
        })
        .WithName("DeleteArtist")
        .WithSummary("Delete an artist")
        .WithDescription("Deletes an artist profile from the system by their ID.")
        .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
