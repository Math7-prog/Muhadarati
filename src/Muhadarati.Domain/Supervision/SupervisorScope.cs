using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Supervision;

/// <summary>
/// نطاق إشراف واحد لمستخدم (مشرف ممكن يكون له أكثر من نطاق)، مع صلاحياته الست ضمن هذا النطاق تحديداً.
/// واحد فقط من أعمدة المعرّفات الأربعة يُملأ حسب ScopeType (قيد CHECK بقاعدة البيانات).
/// </summary>
public class SupervisorScope : IHasCreatedAt, IHasUpdatedAt
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public AcademicScopeType ScopeType { get; set; }

    public Guid? CollegeId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? StageId { get; set; }
    public Guid? SubjectId { get; set; }

    public bool CanUpload { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanPublishNews { get; set; }
    public bool CanManageStructure { get; set; }
    public bool CanHandleReports { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
