using System;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Dsps.Commands.UpdateDsp;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Api.Endpoints.Dsps;

public static class UpdateDsp
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapPut("/{id:guid}", async (Guid id, UpdateDspRequest request, ISender sender) =>
        {
            var command = new UpdateDspCommand(id, request.Name);
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "DSP updated successfully."));
        })
        .WithName("UpdateDsp")
        .WithSummary("Update a DSP")
        .WithDescription("Updates the name of an existing Digital Service Provider (DSP) by their ID.")
        .Produces<ApiResponse<DspDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}

public record UpdateDspRequest(string Name);
