using System;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Tracks.Commands.UpdateTrack;
using TakweneMusic.Application.Tracks.Common;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Domain.Enums;

namespace TakweneMusic.Api.Endpoints.Tracks;

public static class UpdateTrack
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapPut("/{id:guid}", async (Guid id, UpdateTrackRequest request, ISender sender) =>
        {
            var command = new UpdateTrackCommand(
                id,
                request.Title,
                request.ArtistId,
                request.Isrc,
                request.ReleaseDate,
                request.Genre,
                request.Status
            );
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Track updated successfully."));
        })
        .WithName("UpdateTrack")
        .WithSummary("Update a track")
        .WithDescription("Updates the details of an existing track by its ID.")
        .Produces<ApiResponse<TrackDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}

public record UpdateTrackRequest(
    string Title,
    Guid ArtistId,
    string Isrc,
    DateTime ReleaseDate,
    string Genre,
    TrackStatus Status
);
