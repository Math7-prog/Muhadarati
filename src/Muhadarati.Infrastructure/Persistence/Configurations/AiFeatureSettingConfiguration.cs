using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Administration;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class AiFeatureSettingConfiguration : IEntityTypeConfiguration<AiFeatureSetting>
{
    public void Configure(EntityTypeBuilder<AiFeatureSetting> builder)
    {
        builder.HasKey(s => s.FeatureKey);

        builder.Property(s => s.FeatureKey)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(s => s.Provider).IsRequired().HasMaxLength(100);
        builder.Property(s => s.ModelName).IsRequired().HasMaxLength(200);
        builder.Property(s => s.DisplayTitle).IsRequired().HasMaxLength(200);
    }
}
