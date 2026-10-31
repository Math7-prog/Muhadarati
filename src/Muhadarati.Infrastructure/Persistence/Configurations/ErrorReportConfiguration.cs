using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Reports;
using Muhadarati.Infrastructure.Identity;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class ErrorReportConfiguration : IEntityTypeConfiguration<ErrorReport>
{
    public void Configure(EntityTypeBuilder<ErrorReport> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Description).IsRequired();

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(r => r.Lecture)
            .WithMany()
            .HasForeignKey(r => r.LectureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.LectureFile)
            .WithMany()
            .HasForeignKey(r => r.LectureFileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(r => r.ReportedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
