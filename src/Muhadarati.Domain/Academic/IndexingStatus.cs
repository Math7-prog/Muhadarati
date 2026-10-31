namespace Muhadarati.Domain.Academic;

/// <summary>حالة استخراج وفهرسة نص ملف المحاضرة للذكاء الاصطناعي (تمهيداً للمرحلة 2).</summary>
public enum IndexingStatus
{
    Pending,
    Indexing,
    Indexed,
    Failed
}
