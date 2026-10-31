using Muhadarati.Domain.Academic;
using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Students;

/// <summary>ملف الطالب الإضافي — علاقة 1:1 مع المستخدم، بدون Id مستقل.</summary>
public class StudentProfile : IHasCreatedAt, IHasUpdatedAt
{
    public Guid UserId { get; set; }

    /// <summary>المرحلة الأساسية للطالب، قابلة للتغيير لاحقاً.</summary>
    public Guid PrimaryStageId { get; set; }
    public Stage PrimaryStage { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
