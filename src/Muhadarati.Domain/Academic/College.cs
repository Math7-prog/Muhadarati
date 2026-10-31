using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>
/// كلية ضمن الهيكل الأكاديمي. أعلى مستوى بالتسلسل: كلية ← قسم ← مرحلة ← مادة ← محاضرة.
/// </summary>
public class College : AuditableEntity
{
    /// <summary>الاسم بالعربية — الاسم الأساسي، مطلوب دائماً.</summary>
    public string NameAr { get; set; } = string.Empty;

    /// <summary>الاسم بالإنجليزية — اختياري؛ إذا كان فارغاً تعرض الواجهة الإنجليزية NameAr بدلاً عنه.</summary>
    public string? NameEn { get; set; }

    /// <summary>ترتيب ظهور الكلية بالقوائم، يُعدَّل من لوحة التحكم.</summary>
    public int DisplayOrder { get; set; }

    /// <summary>حالة الكلية: ظاهرة / مخفية / مؤرشفة.</summary>
    public EntityStatus Status { get; set; } = EntityStatus.Visible;

    public ICollection<Department> Departments { get; set; } = new List<Department>();
}
