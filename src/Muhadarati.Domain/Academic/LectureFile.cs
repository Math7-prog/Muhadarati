using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Academic;

/// <summary>ملف واحد تابع لمحاضرة (المحاضرة الواحدة قد تحتوي أكثر من ملف).</summary>
public class LectureFile : AuditableEntity
{
    public Guid LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public string OriginalFileName { get; set; } = string.Empty;

    /// <summary>مسار الملف بمساحة التخزين على الخادم (خارج قاعدة البيانات).</summary>
    public string StoragePath { get; set; } = string.Empty;

    public string FileExtension { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }

    /// <summary>مسار نسخة PDF المحوَّلة تلقائياً — فاضٍ إذا الملف أصلاً PDF أو لم يحتَج تحويلاً.</summary>
    public string? ConvertedFilePath { get; set; }

    public IndexingStatus IndexingStatus { get; set; } = IndexingStatus.Pending;
}
