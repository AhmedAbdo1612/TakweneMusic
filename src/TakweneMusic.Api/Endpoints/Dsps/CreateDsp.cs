using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Dsps.Commands.CreateDsp;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Api.Endpoints.Dsps;

public class CreateDsp : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/dsps", async (CreateDspCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "DSP created successfully."));
        })
        .WithName("CreateDsp")
        .WithSummary("Create a new DSP")
        .WithDescription("Adds a new Digital Service Provider (DSP) to the system.")
        .Produces<ApiResponse<DspDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
