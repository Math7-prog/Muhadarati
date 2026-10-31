using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Notifications;

/// <summary>إشعار واحد لمستخدم، مع ربط اختياري بالعنصر المرتبط (محاضرة أو خبر) ليكون قابلاً للضغط.</summary>
public class Notification : IHasCreatedAt
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public NotificationType Type { get; set; }
    public string Message { get; set; } = string.Empty;

    /// <summary>نوع العنصر المرتبط (مثل "Lecture" أو "News") — بدون قيد مفتاح أجنبي، لأن النوع مفتوح.</summary>
    public string? RelatedEntityType { get; set; }
    public Guid? RelatedEntityId { get; set; }

    /// <summary>فاضٍ = غير مقروء؛ تاريخ فعلي = وقت القراءة.</summary>
    public DateTime? ReadAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
