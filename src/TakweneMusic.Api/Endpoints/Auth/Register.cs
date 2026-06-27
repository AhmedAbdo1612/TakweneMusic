using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Users.Commands.RegisterUser;

namespace TakweneMusic.Api.Endpoints.Auth;

public class Register : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register", async (RegisterUserCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Ok(ApiResponse.Success(result, "Registered user successfully."));
        }).AllowAnonymous();
    }
}
