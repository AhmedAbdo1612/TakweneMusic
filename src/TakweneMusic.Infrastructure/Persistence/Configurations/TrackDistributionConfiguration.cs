using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Infrastructure.Persistence.Configurations;

public class TrackDistributionConfiguration : IEntityTypeConfiguration<TrackDistribution>
{
    public void Configure(EntityTypeBuilder<TrackDistribution> builder)
    {
        builder.HasKey(td => td.Id);
        builder.Property(td => td.Status).HasConversion<string>();

        builder.HasOne<Track>()
               .WithMany()
               .HasForeignKey(td => td.TrackId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<Dsp>()
               .WithMany()
               .HasForeignKey(td => td.DspId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
