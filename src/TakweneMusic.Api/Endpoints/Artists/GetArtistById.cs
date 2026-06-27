using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Artists.Queries.GetArtistById;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Endpoints.Artists;

public class GetArtistById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/artists/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetArtistByIdQuery(id));
            return Results.Ok(ApiResponse.Success(result));
        }).RequireAuthorization();
    }
}
