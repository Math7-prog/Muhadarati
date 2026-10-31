using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Academic;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class SubjectStageLinkConfiguration : IEntityTypeConfiguration<SubjectStageLink>
{
    public void Configure(EntityTypeBuilder<SubjectStageLink> builder)
    {
        builder.HasKey(l => new { l.SubjectId, l.StageId });

        builder.HasOne(l => l.Subject)
            .WithMany(s => s.AdditionalStageLinks)
            .HasForeignKey(l => l.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.Stage)
            .WithMany(s => s.AdditionalSubjectLinks)
            .HasForeignKey(l => l.StageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
