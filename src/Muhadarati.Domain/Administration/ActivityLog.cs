using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Administration;

/// <summary>
/// سجل نشاط واحد (من نفّذ ماذا على أي عنصر ومتى). entity_type مفتوح (أنواع عنصر كثيرة)،
/// فـ entity_id بدون قيد مفتاح أجنبي حقيقي — استثناء مقصود، غير مثل الجداول محصورة الأنواع الأربعة.
/// </summary>
public class ActivityLog : IHasCreatedAt
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ActivityAction Action { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public Guid EntityId { get; set; }

    public string? Details { get; set; }

    public DateTime CreatedAt { get; set; }
}
