using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Api.Endpoints.Common;

namespace TakweneMusic.Api.Endpoints.Tracks;

public class TracksGroup : IEndpointGroup
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/tracks").WithTags("Tracks");
        
        CreateTrack.MapEndpoint(group);
        DeleteTrack.MapEndpoint(group);
        GetTrackById.MapEndpoint(group);
        GetTracks.MapEndpoint(group);
        UpdateTrack.MapEndpoint(group);
    }
}
