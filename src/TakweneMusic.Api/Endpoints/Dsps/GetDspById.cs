using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Dsps.Queries.GetDspById;

namespace TakweneMusic.Api.Endpoints.Dsps;

public class GetDspById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dsps/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetDspByIdQuery(id));
            return Results.Ok(ApiResponse.Success(result));
        }).RequireAuthorization();
    }
}
