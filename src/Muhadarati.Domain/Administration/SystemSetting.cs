using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Administration;

/// <summary>إعداد عام مفتاح/قيمة (حدود الملفات، التشغيل، البريد...)، القيمة نص يحوّله الكود حسب المفتاح.</summary>
public class SystemSetting : IHasUpdatedAt
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; }
}
