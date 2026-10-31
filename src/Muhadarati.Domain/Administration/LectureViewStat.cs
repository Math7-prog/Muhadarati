using Muhadarati.Domain.Academic;
using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Administration;

/// <summary>عدّادات مشاهدة وتحميل محاضرة واحدة — علاقة 1:1 معها، بدون Id مستقل.</summary>
public class LectureViewStat : IHasUpdatedAt
{
    public Guid LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public long ViewCount { get; set; }
    public long DownloadCount { get; set; }

    public DateTime UpdatedAt { get; set; }
}
