using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Users.Commands.RotateRefreshToken;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Api.Endpoints.Auth;

public class Refresh : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/refresh", async (RotateRefreshTokenCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Token refreshed successfully."));
        })
        .WithName("Refresh")
        .WithSummary("Refresh authentication token")
        .WithDescription("Rotates a valid, unexpired refresh token, returning a new JWT token and a new refresh token.")
        .Produces<ApiResponse<AuthResponseDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .AllowAnonymous();
    }
}
