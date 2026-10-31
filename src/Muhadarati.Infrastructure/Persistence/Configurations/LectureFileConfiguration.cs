using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Academic;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class LectureFileConfiguration : IEntityTypeConfiguration<LectureFile>
{
    public void Configure(EntityTypeBuilder<LectureFile> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.OriginalFileName).IsRequired().HasMaxLength(300);
        builder.Property(f => f.StoragePath).IsRequired().HasMaxLength(1000);
        builder.Property(f => f.FileExtension).IsRequired().HasMaxLength(20);

        builder.Property(f => f.IndexingStatus)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(f => f.Lecture)
            .WithMany()
            .HasForeignKey(f => f.LectureId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
