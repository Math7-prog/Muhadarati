using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>
/// قسم تابع لكلية واحدة، ويحتوي على مراحل دراسية.
/// </summary>
public class Department : AuditableEntity
{
    /// <summary>الكلية التي يتبعها القسم — كل قسم يتبع كلية واحدة إلزامياً.</summary>
    public Guid CollegeId { get; set; }
    public College College { get; set; } = null!;

    /// <summary>الاسم بالعربية — مطلوب دائماً.</summary>
    public string NameAr { get; set; } = string.Empty;

    /// <summary>الاسم بالإنجليزية — اختياري.</summary>
    public string? NameEn { get; set; }

    public int DisplayOrder { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Visible;

    public ICollection<Stage> Stages { get; set; } = new List<Stage>();
}
