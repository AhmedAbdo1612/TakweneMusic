using System;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Dsps.Commands.UpdateDsp;

namespace TakweneMusic.Api.Endpoints.Dsps;

public class UpdateDsp : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/dsps/{id:guid}", async (Guid id, UpdateDspRequest request, ISender sender) =>
        {
            var command = new UpdateDspCommand(id, request.Name);
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "DSP updated successfully."));
        }).RequireAuthorization();
    }
}

public record UpdateDspRequest(string Name);
