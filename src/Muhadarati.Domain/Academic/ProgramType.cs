namespace Muhadarati.Domain.Academic;

/// <summary>
/// نوع البرنامج الذي تخص به نسخة المحاضرة، حسب القسم 4.2 من مواصفة المشروع.
/// </summary>
public enum ProgramType
{
    /// <summary>مشتركة بين الدراستين الصباحية والمسائية (الحالة الافتراضية).</summary>
    Shared,

    /// <summary>نسخة خاصة بالدراسة الصباحية فقط، عند اختلاف المحتوى عن المسائية.</summary>
    Morning,

    /// <summary>نسخة خاصة بالدراسة المسائية فقط، عند اختلاف المحتوى عن الصباحية.</summary>
    Evening
}
