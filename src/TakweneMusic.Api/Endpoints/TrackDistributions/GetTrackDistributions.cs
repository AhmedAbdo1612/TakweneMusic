using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.TrackDistributions.Queries.GetTrackDistributions;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public class GetTrackDistributions : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/track-distributions", async (ISender sender) =>
        {
            var result = await sender.Send(new GetTrackDistributionsQuery());
            return Results.Ok(ApiResponse.Success(result));
        }).RequireAuthorization();
    }
}
