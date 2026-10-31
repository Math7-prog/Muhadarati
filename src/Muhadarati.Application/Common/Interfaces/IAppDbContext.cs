using Microsoft.EntityFrameworkCore;
using Muhadarati.Domain.Academic;

namespace Muhadarati.Application.Common.Interfaces;

/// <summary>
/// تجريد لقاعدة البيانات تعتمد عليه طبقة Application، بدون أي إشارة لـ Npgsql أو أي تفصيل بنية تحتية.
/// التنفيذ الفعلي (AppDbContext) موجود بطبقة Infrastructure.
/// </summary>
public interface IAppDbContext
{
    DbSet<College> Colleges { get; }
    DbSet<Department> Departments { get; }
    DbSet<Stage> Stages { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<SubjectStageLink> SubjectStageLinks { get; }
    DbSet<Lecture> Lectures { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
