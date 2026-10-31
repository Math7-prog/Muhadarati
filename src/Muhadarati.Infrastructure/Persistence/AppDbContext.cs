using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Muhadarati.Application.Common.Interfaces;
using Muhadarati.Domain.Academic;
using Muhadarati.Domain.Administration;
using Muhadarati.Domain.Announcements;
using Muhadarati.Domain.Common;
using Muhadarati.Domain.Notifications;
using Muhadarati.Domain.Reports;
using Muhadarati.Domain.Students;
using Muhadarati.Domain.Supervision;
using Muhadarati.Infrastructure.Identity;

namespace Muhadarati.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<College> Colleges => Set<College>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Stage> Stages => Set<Stage>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<SubjectStageLink> SubjectStageLinks => Set<SubjectStageLink>();
    public DbSet<Lecture> Lectures => Set<Lecture>();
    public DbSet<LectureFile> LectureFiles => Set<LectureFile>();
    public DbSet<LectureVideoLink> LectureVideoLinks => Set<LectureVideoLink>();

    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<StudentPinnedSubject> StudentPinnedSubjects => Set<StudentPinnedSubject>();

    public DbSet<SupervisorScope> SupervisorScopes => Set<SupervisorScope>();

    public DbSet<News> News => Set<News>();
    public DbSet<NewsTarget> NewsTargets => Set<NewsTarget>();

    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<NotificationPreference> NotificationPreferences => Set<NotificationPreference>();

    public DbSet<ErrorReport> ErrorReports => Set<ErrorReport>();

    public DbSet<AiFeatureSetting> AiFeatureSettings => Set<AiFeatureSetting>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();
    public DbSet<LectureViewStat> LectureViewStats => Set<LectureViewStat>();
    public DbSet<AiUsageStat> AiUsageStats => Set<AiUsageStat>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // يجب استدعاؤها أولاً عند الوراثة من IdentityDbContext، حتى تُهيَّأ جداول Identity نفسها.
        base.OnModelCreating(modelBuilder);

        // أسماء جداول صديقة بدل بادئة AspNet الافتراضية (تطابق تسمية المواصفة: Users, Roles...).
        modelBuilder.Entity<ApplicationUser>().ToTable("users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("roles");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");

        // تفعيل امتداد pgvector من الآن (قرار متفق عليه) حتى لو جدول المتجهات نفسه يُضاف لاحقاً بالمرحلة 2.
        modelBuilder.HasPostgresExtension("vector");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IHasCreatedAt>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IHasUpdatedAt>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = utcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
