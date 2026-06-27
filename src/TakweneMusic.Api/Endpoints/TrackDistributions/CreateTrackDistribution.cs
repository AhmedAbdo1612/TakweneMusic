using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Commands.CreateTrackDistribution;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public class CreateTrackDistribution : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/track-distributions", async (CreateTrackDistributionCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Track distribution created successfully."));
        }).RequireAuthorization();
    }
}
