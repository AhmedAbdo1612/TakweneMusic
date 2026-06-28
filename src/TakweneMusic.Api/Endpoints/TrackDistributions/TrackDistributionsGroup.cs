using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Api.Endpoints.Common;

namespace TakweneMusic.Api.Endpoints.TrackDistributions;

public class TrackDistributionsGroup : IEndpointGroup
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/track-distributions").WithTags("TrackDistributions");
        
        CreateTrackDistribution.MapEndpoint(group);
        DeleteTrackDistribution.MapEndpoint(group);
        GetTrackDistributionById.MapEndpoint(group);
        GetTrackDistributions.MapEndpoint(group);
        UpdateTrackDistribution.MapEndpoint(group);
    }
}
