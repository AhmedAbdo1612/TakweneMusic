using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Tracks.Commands.CreateTrack;
using TakweneMusic.Application.Tracks.Common;

namespace TakweneMusic.Api.Endpoints.Tracks;

public class CreateTrack : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tracks", async (CreateTrackCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Track created successfully."));
        })
        .WithName("CreateTrack")
        .WithSummary("Create a new track")
        .WithDescription("Creates a new track record, defining title, genre, release date, and associating it with an artist ID.")
        .Produces<ApiResponse<TrackDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
