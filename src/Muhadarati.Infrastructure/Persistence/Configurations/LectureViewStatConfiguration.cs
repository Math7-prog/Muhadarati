using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Administration;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class LectureViewStatConfiguration : IEntityTypeConfiguration<LectureViewStat>
{
    public void Configure(EntityTypeBuilder<LectureViewStat> builder)
    {
        builder.HasKey(s => s.LectureId);

        builder.HasOne(s => s.Lecture)
            .WithMany()
            .HasForeignKey(s => s.LectureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
