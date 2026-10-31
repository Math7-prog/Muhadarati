using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Announcements;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class NewsTargetConfiguration : IEntityTypeConfiguration<NewsTarget>
{
    public void Configure(EntityTypeBuilder<NewsTarget> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TargetType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(t => t.News)
            .WithMany(n => n.Targets)
            .HasForeignKey(t => t.NewsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => new { t.NewsId, t.CollegeId })
            .IsUnique()
            .HasFilter("target_type = 'College'");

        builder.HasIndex(t => new { t.NewsId, t.DepartmentId })
            .IsUnique()
            .HasFilter("target_type = 'Department'");

        builder.HasIndex(t => new { t.NewsId, t.StageId })
            .IsUnique()
            .HasFilter("target_type = 'Stage'");

        builder.HasIndex(t => new { t.NewsId, t.SubjectId })
            .IsUnique()
            .HasFilter("target_type = 'Subject'");

        builder.ToTable(t => t.HasCheckConstraint(
            "ck_news_targets_single_target",
            "num_nonnulls(college_id, department_id, stage_id, subject_id) = 1"));
    }
}
