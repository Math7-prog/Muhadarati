using Muhadarati.Domain.Academic;
using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Students;

/// <summary>مادة إضافية ثبّتها الطالب بصفحته الرئيسية (من مرحلة غير مرحلته الأساسية عادة).</summary>
public class StudentPinnedSubject : IHasCreatedAt
{
    public Guid UserId { get; set; }

    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
