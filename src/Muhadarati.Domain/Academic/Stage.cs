using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>
/// مرحلة دراسية تابعة لقسم واحد (مثل: المرحلة الأولى، الثانية...).
/// </summary>
public class Stage : AuditableEntity
{
    /// <summary>القسم الذي تتبعه المرحلة.</summary>
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public string NameAr { get; set; } = string.Empty;
    public string? NameEn { get; set; }

    /// <summary>رقم المرحلة الدراسي الفعلي (1، 2، 3، 4...) — يختلف عن DisplayOrder الذي هو لترتيب العرض فقط.</summary>
    public int Number { get; set; }

    public int DisplayOrder { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Visible;

    /// <summary>المواد التي هذي المرحلة هي "المرحلة المالكة" لها.</summary>
    public ICollection<Subject> OwnedSubjects { get; set; } = new List<Subject>();

    /// <summary>روابط المواد المشتركة التي تظهر بهذي المرحلة كمرحلة إضافية (غير مالكة).</summary>
    public ICollection<SubjectStageLink> AdditionalSubjectLinks { get; set; } = new List<SubjectStageLink>();
}
