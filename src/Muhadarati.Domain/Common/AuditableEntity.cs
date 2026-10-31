namespace Muhadarati.Domain.Common;

/// <summary>
/// القاعدة المشتركة لكل كيانات الهيكل الأكاديمي: معرّف فريد وتوقيتا الإنشاء والتعديل.
/// </summary>
public abstract class AuditableEntity : IHasCreatedAt, IHasUpdatedAt
{
    public Guid Id { get; set; }

    /// <summary>وقت إنشاء السجل (UTC)، يُنملى تلقائياً عند الحفظ ولا يُعدَّل بعد ذلك.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>وقت آخر تعديل على السجل (UTC)، يُحدَّث تلقائياً بكل حفظ.</summary>
    public DateTime UpdatedAt { get; set; }
}
