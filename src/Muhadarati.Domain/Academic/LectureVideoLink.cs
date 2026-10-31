using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>رابط فيديو YouTube غير مدرَج تابع لمحاضرة، كبديل لرفع ملف فيديو.</summary>
public class LectureVideoLink : AuditableEntity
{
    public Guid LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public string Url { get; set; } = string.Empty;
    public string? Title { get; set; }
}
