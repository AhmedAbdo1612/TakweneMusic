using Microsoft.EntityFrameworkCore;
using TakweneMusic.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace TakweneMusic.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Artist> Artists { get; }
    DbSet<Track> Tracks { get; }
    DbSet<Dsp> Dsps { get; }
    DbSet<TrackDistribution> TrackDistributions { get; }
    DbSet<ApplicationUser> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
