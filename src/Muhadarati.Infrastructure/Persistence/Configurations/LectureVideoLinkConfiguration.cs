using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Academic;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class LectureVideoLinkConfiguration : IEntityTypeConfiguration<LectureVideoLink>
{
    public void Configure(EntityTypeBuilder<LectureVideoLink> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Url).IsRequired().HasMaxLength(2000);
        builder.Property(v => v.Title).HasMaxLength(300);

        builder.HasOne(v => v.Lecture)
            .WithMany()
            .HasForeignKey(v => v.LectureId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
