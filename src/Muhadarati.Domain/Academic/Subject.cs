using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>
/// مادة دراسية تتبع مرحلة مالكة واحدة، وقد تظهر بمراحل إضافية عبر SubjectStageLink.
/// </summary>
public class Subject : AuditableEntity
{
    /// <summary>المرحلة المالكة: المسؤولة إدارياً عن المادة ورفع محاضراتها.</summary>
    public Guid OwningStageId { get; set; }
    public Stage OwningStage { get; set; } = null!;

    public string NameAr { get; set; } = string.Empty;
    public string? NameEn { get; set; }

    /// <summary>وصف تفصيلي اختياري يظهر بصفحة المادة.</summary>
    public string? DescriptionAr { get; set; }
    public string? DescriptionEn { get; set; }

    public int DisplayOrder { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Visible;

    /// <summary>مراحل إضافية (غير المالكة) تظهر فيها نفس محاضرات هذي المادة.</summary>
    public ICollection<SubjectStageLink> AdditionalStageLinks { get; set; } = new List<SubjectStageLink>();

    public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}
