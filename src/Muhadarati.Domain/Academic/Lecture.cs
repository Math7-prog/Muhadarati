using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>
/// محاضرة تتبع مادة واحدة. هذي الخطوة تضيف بيانات المحاضرة الوصفية فقط؛
/// ملفاتها (LectureFiles) تُضاف بخطوة لاحقة منفصلة.
/// </summary>
public class Lecture : AuditableEntity
{
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    /// <summary>المستخدم (مشرف/أدمن) الذي رفع المحاضرة.</summary>
    public Guid UploadedByUserId { get; set; }

    public string TitleAr { get; set; } = string.Empty;
    public string? TitleEn { get; set; }

    public string? DescriptionAr { get; set; }
    public string? DescriptionEn { get; set; }

    /// <summary>ترتيب المحاضرة ضمن مادتها (محاضرة 1، 2، 3...).</summary>
    public int Number { get; set; }

    /// <summary>نوع البرنامج: مشتركة، أو نسخة خاصة بالصباحي/المسائي.</summary>
    public ProgramType ProgramType { get; set; } = ProgramType.Shared;

    /// <summary>حالة المحاضرة: ظاهرة / مخفية / مؤرشفة (إضافة متفَق عليها فوق المواصفة الأصلية).</summary>
    public EntityStatus Status { get; set; } = EntityStatus.Visible;
}
