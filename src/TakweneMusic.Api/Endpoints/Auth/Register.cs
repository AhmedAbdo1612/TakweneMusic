using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Users.Commands.RegisterUser;
using TakweneMusic.Application.Users.Common;

namespace TakweneMusic.Api.Endpoints.Auth;

public static class Register
{
    public static void MapEndpoint(this RouteGroupBuilder app)
    {
        app.MapPost("/register", async (RegisterUserCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Registered user successfully."));
        })
        .WithName("Register")
        .WithSummary("User registration")
        .WithDescription("Creates a new user account with a username, email, and password.")
        .Produces<ApiResponse<AuthResponseDto>>(StatusCodes.Status200OK)
        .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
        .AllowAnonymous();
    }
}
