using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TakweneMusic.Api.Endpoints.Common;

namespace TakweneMusic.Api.Endpoints.Artists;

public class ArtistsGroup : IEndpointGroup
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/artists").WithTags("Artists");
        
        CreateArtist.MapEndpoint(group);
        DeleteArtist.MapEndpoint(group);
        GetArtistById.MapEndpoint(group);
        GetArtists.MapEndpoint(group);
        UpdateArtist.MapEndpoint(group);
    }
}
