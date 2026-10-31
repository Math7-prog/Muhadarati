using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Administration;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class AiUsageStatConfiguration : IEntityTypeConfiguration<AiUsageStat>
{
    public void Configure(EntityTypeBuilder<AiUsageStat> builder)
    {
        builder.HasKey(s => new { s.FeatureKey, s.Date });

        builder.Property(s => s.FeatureKey)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.HasOne<AiFeatureSetting>()
            .WithMany()
            .HasForeignKey(s => s.FeatureKey)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
