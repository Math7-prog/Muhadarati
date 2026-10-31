using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Academic;
using Muhadarati.Infrastructure.Identity;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
{
    public void Configure(EntityTypeBuilder<Lecture> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.TitleAr)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(l => l.TitleEn)
            .HasMaxLength(300);

        builder.Property(l => l.ProgramType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(l => l.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(l => new { l.SubjectId, l.Number, l.ProgramType })
            .IsUnique();

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(l => l.UploadedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
