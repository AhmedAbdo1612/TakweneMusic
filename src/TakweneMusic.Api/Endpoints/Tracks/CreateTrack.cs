using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Tracks.Commands.CreateTrack;

namespace TakweneMusic.Api.Endpoints.Tracks;

public class CreateTrack : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tracks", async (CreateTrackCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Track created successfully."));
        }).RequireAuthorization();
    }
}
