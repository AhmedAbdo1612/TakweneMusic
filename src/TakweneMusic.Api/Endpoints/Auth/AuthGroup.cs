using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Api.Endpoints.Common;

namespace TakweneMusic.Api.Endpoints.Auth;

public class AuthGroup : IEndpointGroup
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth").WithTags("Auth");
        
        Login.MapEndpoint(group);
        Refresh.MapEndpoint(group);
        Register.MapEndpoint(group);
    }
}
