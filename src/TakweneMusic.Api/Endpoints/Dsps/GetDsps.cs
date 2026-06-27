using System.Collections.Generic;
using System.Threading.Tasks;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Application.Common.Models;
using TakweneMusic.Application.Dsps.Common;
using TakweneMusic.Application.Dsps.Queries.GetDsps;

namespace TakweneMusic.Api.Endpoints.Dsps;

public class GetDsps : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dsps", async (ISender sender) =>
        {
            var result = await sender.Send(new GetDspsQuery());
            return Results.Ok(ApiResponse.Success(result));
        })
        .WithName("GetDsps")
        .WithSummary("Retrieve all DSPs")
        .WithDescription("Fetches a list of all Digital Service Providers (DSPs) available in the system.")
        .Produces<ApiResponse<List<DspDto>>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
