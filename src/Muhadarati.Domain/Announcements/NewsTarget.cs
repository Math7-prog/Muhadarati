using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Announcements;

/// <summary>فئة واحدة يستهدفها خبر (خبر واحد ممكن يستهدف أكثر من فئة). نفس نمط CHECK لـ SupervisorScope.</summary>
public class NewsTarget : IHasCreatedAt
{
    public Guid Id { get; set; }

    public Guid NewsId { get; set; }
    public News News { get; set; } = null!;

    public AcademicScopeType TargetType { get; set; }

    public Guid? CollegeId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? StageId { get; set; }
    public Guid? SubjectId { get; set; }

    public DateTime CreatedAt { get; set; }
}
