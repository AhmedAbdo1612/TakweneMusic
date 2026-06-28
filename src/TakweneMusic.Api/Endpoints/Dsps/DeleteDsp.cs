using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Dsps.Commands.DeleteDsp;

namespace TakweneMusic.Api.Endpoints.Dsps;

public static class DeleteDsp
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteDspCommand(id));
            return Results.Ok(ApiResponse.Success(result, "DSP deleted successfully."));
        })
        .WithName("DeleteDsp")
        .WithSummary("Delete a DSP")
        .WithDescription("Deletes a Digital Service Provider (DSP) from the system by their ID.")
        .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
