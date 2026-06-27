using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Commands.DeleteTrackDistribution;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public class DeleteTrackDistribution : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/track-distributions/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTrackDistributionCommand(id));
            return Results.Ok(ApiResponse.Success(result, "Track distribution deleted successfully."));
        }).RequireAuthorization();
    }
}
