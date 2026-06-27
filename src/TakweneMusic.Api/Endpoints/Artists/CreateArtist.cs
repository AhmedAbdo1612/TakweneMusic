using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Commands.CreateArtist;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public class CreateArtist : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/artists", async (CreateArtistCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Artist created successfully."));
        }).RequireAuthorization();
    }
}
