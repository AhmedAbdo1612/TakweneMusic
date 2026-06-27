using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        var (statusCode, title, detail, errors) = exception switch
        {
            ValidationException valEx => (
                StatusCodes.Status400BadRequest,
                "Validation Error",
                "One or more validation failures occurred.",
                valEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    )
            ),
            ArgumentException argEx => (
                StatusCodes.Status400BadRequest,
                "Bad Request",
                argEx.Message,
                null
            ),
            UnauthorizedAccessException authEx => (
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                authEx.Message,
                null
            ),
            KeyNotFoundException knfEx => (
                StatusCodes.Status404NotFound,
                "Not Found",
                knfEx.Message,
                null
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred on the server.",
                null
            )
        };

        httpContext.Response.StatusCode = statusCode;

        if (errors != null)
        {
            var validationProblemDetails = new HttpValidationProblemDetails(errors)
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };
            
            var response = ApiResponse.Failure(detail, validationProblemDetails);
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        }
        else
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            };

            var response = ApiResponse.Failure(detail, problemDetails);
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        }

        return true;
    }
}
