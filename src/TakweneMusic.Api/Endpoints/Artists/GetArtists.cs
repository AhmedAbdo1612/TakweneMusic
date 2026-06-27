using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Queries.GetArtists;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public class GetArtists : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/artists", async (ISender sender) =>
        {
            var result = await sender.Send(new GetArtistsQuery());
            return Results.Ok(ApiResponse.Success(result));
        }).RequireAuthorization();
    }
}
