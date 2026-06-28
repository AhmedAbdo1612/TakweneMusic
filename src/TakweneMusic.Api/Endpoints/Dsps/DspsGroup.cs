using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Api.Endpoints.Common;

namespace TakweneMusic.Api.Endpoints.Dsps;

public class DspsGroup : IEndpointGroup
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/dsps").WithTags("Dsps");
        
        CreateDsp.MapEndpoint(group);
        DeleteDsp.MapEndpoint(group);
        GetDspById.MapEndpoint(group);
        GetDsps.MapEndpoint(group);
        UpdateDsp.MapEndpoint(group);
    }
}
