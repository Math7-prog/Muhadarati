using Microsoft.AspNetCore.Identity;

namespace Muhadarati.Infrastructure.Identity;

/// <summary>
/// مستخدم المنصة، فوق IdentityUser&lt;Guid&gt; القياسي. يعيش بطبقة Infrastructure حصراً
/// (قرار معماري): Domain لا يشير له إطلاقاً، وكيانات Domain الأخرى تخزن Guid UserId فقط.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = string.Empty;
    public PreferredLanguage PreferredLanguage { get; set; } = PreferredLanguage.Arabic;
    public UserTheme Theme { get; set; } = UserTheme.Auto;

    /// <summary>فاضٍ حتى يوثّق الطالب بريده الجامعي (القسم 3.1).</summary>
    public string? UniversityEmail { get; set; }

    /// <summary>فاضٍ = لم يُوثَّق بعد؛ تاريخ فعلي = وقت نجاح التحقق بـOTP (يفتح ميزات الذكاء الاصطناعي).</summary>
    public DateTime? UniversityEmailVerifiedAt { get; set; }
}
