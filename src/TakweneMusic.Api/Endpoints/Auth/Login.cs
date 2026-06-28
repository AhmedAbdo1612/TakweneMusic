using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Users.Commands.LoginUser;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Api.Endpoints.Auth;

public static class Login
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapPost("/login", async (LoginUserCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Logged in successfully."));
        })
        .WithName("Login")
        .WithSummary("User login")
        .WithDescription("Authenticates a user with email and password, returning a JWT token and a refresh token.")
        .Produces<ApiResponse<AuthResponseDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .AllowAnonymous();
    }
}
