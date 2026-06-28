using Microsoft.AspNetCore.Routing;

namespace TakweneMusic.Api.Endpoints.Common;

public interface IEndpointGroup
{
    void MapEndpoints(IEndpointRouteBuilder app);
}
