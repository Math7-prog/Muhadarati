using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Supervision;
using Muhadarati.Infrastructure.Identity;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class SupervisorScopeConfiguration : IEntityTypeConfiguration<SupervisorScope>
{
    public void Configure(EntityTypeBuilder<SupervisorScope> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.ScopeType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // فهارس فريدة جزئية (Partial) — واحد لكل نوع نطاق، لأن فهرساً مركّباً واحداً لا يعمل
        // بشكل صحيح مع أعمدة Nullable (Postgres يعتبر NULL قيمة مختلفة عن NULL أخرى، فلا يمنع التكرار فعلياً).
        builder.HasIndex(s => new { s.UserId, s.CollegeId })
            .IsUnique()
            .HasFilter("scope_type = 'College'");

        builder.HasIndex(s => new { s.UserId, s.DepartmentId })
            .IsUnique()
            .HasFilter("scope_type = 'Department'");

        builder.HasIndex(s => new { s.UserId, s.StageId })
            .IsUnique()
            .HasFilter("scope_type = 'Stage'");

        builder.HasIndex(s => new { s.UserId, s.SubjectId })
            .IsUnique()
            .HasFilter("scope_type = 'Subject'");

        builder.ToTable(t => t.HasCheckConstraint(
            "ck_supervisor_scopes_single_scope",
            "num_nonnulls(college_id, department_id, stage_id, subject_id) = 1"));
    }
}
