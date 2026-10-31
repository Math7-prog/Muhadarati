namespace Muhadarati.Domain.Common;

/// <summary>
/// الحالة العامة لعناصر الهيكل الأكاديمي والمحاضرات، تُستخدم بدلاً من الحذف الفعلي.
/// </summary>
public enum EntityStatus
{
    /// <summary>ظاهر للجميع (الطلاب والزوار حسب الصلاحية).</summary>
    Visible,

    /// <summary>مخفي مؤقتاً عن الطلاب والزوار، ويبقى ظاهراً للمشرفين بلوحة التحكم.</summary>
    Hidden,

    /// <summary>مؤرشف نهائياً: انتهى العمل به لكنه يبقى محفوظاً بسجلاته القديمة.</summary>
    Archived
}
