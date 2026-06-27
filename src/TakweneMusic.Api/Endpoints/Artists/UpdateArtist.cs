using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Commands.UpdateArtist;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public class UpdateArtist : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/artists/{id:guid}", async (Guid id, UpdateArtistRequest request, ISender sender) =>
        {
            var command = new UpdateArtistCommand(id, request.Name, request.Email, request.Country);
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Artist updated successfully."));
        }).RequireAuthorization();
    }
}

public record UpdateArtistRequest(string Name, string Email, string Country);
