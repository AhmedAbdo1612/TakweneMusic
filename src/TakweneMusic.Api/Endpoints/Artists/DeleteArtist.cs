using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
        }).RequireAuthorization();
    }
}
