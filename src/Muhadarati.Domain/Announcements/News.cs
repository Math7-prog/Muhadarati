using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Announcements;

/// <summary>خبر أو إعلان أكاديمي. العنوان والمحتوى حقل حر بلغة واحدة يختارها الناشر.</summary>
public class News : IHasCreatedAt, IHasUpdatedAt
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    /// <summary>المستخدم (مشرف/أدمن) الناشر.</summary>
    public Guid PublishedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<NewsTarget> Targets { get; set; } = new List<NewsTarget>();
}
