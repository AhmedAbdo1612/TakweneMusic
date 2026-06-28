using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Dsps.Common;
using TakweneMusic.Application.Dsps.Queries.GetDspById;

namespace TakweneMusic.Api.Endpoints.Dsps;

public static class GetDspById
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetDspByIdQuery(id));
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetDspById")
        .WithSummary("Retrieve a DSP by ID")
        .WithDescription("Fetches details of a specific Digital Service Provider (DSP) by their unique identifier.")
        .Produces<ApiResponse<DspDto>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
