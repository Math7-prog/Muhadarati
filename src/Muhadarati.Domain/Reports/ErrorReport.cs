using Muhadarati.Domain.Academic;
using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Reports;

/// <summary>بلاغ طالب عن خطأ بمحاضرة أو ملف تالف/ناقص.</summary>
public class ErrorReport : IHasCreatedAt, IHasUpdatedAt
{
    public Guid Id { get; set; }

    public Guid LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    /// <summary>ملف محدد إذا كان البلاغ عن ملف بعينه، لا المحاضرة عموماً.</summary>
    public Guid? LectureFileId { get; set; }
    public LectureFile? LectureFile { get; set; }

    public Guid ReportedByUserId { get; set; }

    public string Description { get; set; } = string.Empty;
    public ErrorReportStatus Status { get; set; } = ErrorReportStatus.New;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
