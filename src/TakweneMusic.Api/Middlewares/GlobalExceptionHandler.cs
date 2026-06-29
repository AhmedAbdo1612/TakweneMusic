using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TakweneMusic.Application.Common.Models;

namespace TakweneMusic.Api.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred: {Message}\n\n", exception.Message);

        var (statusCode, message) = exception switch
        {
            ValidationException valEx => (
                StatusCodes.Status400BadRequest,

                string.Join(" ", valEx.Errors.Select(x => x.ErrorMessage))

            ),
            ArgumentException argEx => (
                StatusCodes.Status400BadRequest,

                argEx.Message

            ),
            UnauthorizedAccessException authEx => (
                StatusCodes.Status401Unauthorized,
                "Unauthorized"


            ),
            KeyNotFoundException knfEx => (
                StatusCodes.Status404NotFound,

                knfEx.Message

            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error, try again later"
            )
        };
        var response = ApiResponse<bool>.Failure(message);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
