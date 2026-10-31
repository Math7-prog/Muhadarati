using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>
/// جدول ربط صرف: يربط مادة مشتركة بمرحلة إضافية غير مرحلتها المالكة،
/// حتى تظهر محاضراتها تلقائياً بأكثر من مرحلة دون تكرار الرفع.
/// مفتاحه الأساسي مركّب (SubjectId, StageId) بدون Id مستقل، لأنه علاقة لا كيان مستقل.
/// </summary>
public class SubjectStageLink : IHasCreatedAt
{
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    /// <summary>المرحلة الإضافية — يجب أن تختلف عن OwningStageId لنفس المادة (يُتحقق منه بالتطبيق).</summary>
    public Guid StageId { get; set; }
    public Stage Stage { get; set; } = null!;

    /// <summary>وقت إنشاء الربط (UTC).</summary>
    public DateTime CreatedAt { get; set; }
}
