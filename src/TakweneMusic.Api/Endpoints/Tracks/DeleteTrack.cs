using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Tracks.Commands.DeleteTrack;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Tracks;

public static class DeleteTrack
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTrackCommand(id));
            return Results.Ok(ApiResponse.Success(result, "Track deleted successfully."));
        })
        .WithName("DeleteTrack")
        .WithSummary("Delete a track")
        .WithDescription("Deletes a track record from the system by its ID.")
        .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
