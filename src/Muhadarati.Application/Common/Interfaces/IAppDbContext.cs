using Microsoft.EntityFrameworkCore;
using Muhadarati.Domain.Academic;
using Muhadarati.Domain.Administration;
using Muhadarati.Domain.Announcements;
using Muhadarati.Domain.Notifications;
using Muhadarati.Domain.Reports;
using Muhadarati.Domain.Students;
using Muhadarati.Domain.Supervision;

namespace Muhadarati.Application.Common.Interfaces;

/// <summary>
/// تجريد لقاعدة البيانات تعتمد عليه طبقة Application، بدون أي إشارة لـ Npgsql أو ASP.NET Identity.
/// التنفيذ الفعلي (AppDbContext) موجود بطبقة Infrastructure. المستخدمون (Users/Roles) لا يظهرون هنا
/// عمداً — يُتعامل معهم عبر UserManager/RoleManager القياسيين، لا عبر هذا التجريد.
/// </summary>
public interface IAppDbContext
{
    DbSet<College> Colleges { get; }
    DbSet<Department> Departments { get; }
    DbSet<Stage> Stages { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<SubjectStageLink> SubjectStageLinks { get; }
    DbSet<Lecture> Lectures { get; }
    DbSet<LectureFile> LectureFiles { get; }
    DbSet<LectureVideoLink> LectureVideoLinks { get; }

    DbSet<StudentProfile> StudentProfiles { get; }
    DbSet<StudentPinnedSubject> StudentPinnedSubjects { get; }

    DbSet<SupervisorScope> SupervisorScopes { get; }

    DbSet<News> News { get; }
    DbSet<NewsTarget> NewsTargets { get; }

    DbSet<Notification> Notifications { get; }
    DbSet<NotificationPreference> NotificationPreferences { get; }

    DbSet<ErrorReport> ErrorReports { get; }

    DbSet<AiFeatureSetting> AiFeatureSettings { get; }
    DbSet<SystemSetting> SystemSettings { get; }
    DbSet<LectureViewStat> LectureViewStats { get; }
    DbSet<AiUsageStat> AiUsageStats { get; }
    DbSet<ActivityLog> ActivityLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
