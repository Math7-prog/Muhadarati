using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Notifications;

/// <summary>تفضيلات إشعار المستخدم — علاقة 1:1 معه، بدون Id مستقل.</summary>
public class NotificationPreference : IHasCreatedAt, IHasUpdatedAt
{
    public Guid UserId { get; set; }

    public EmailNotificationPreference EmailPreference { get; set; } = EmailNotificationPreference.All;

    /// <summary>تفعيل الملخص اليومي بدل عدة رسائل منفصلة.</summary>
    public bool DailyDigestEnabled { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
