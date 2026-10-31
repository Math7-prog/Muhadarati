namespace Muhadarati.Domain.Common;

/// <summary>
/// يطبَّق على أي كيان يحتاج طابع وقت آخر تعديل تلقائي (UTC)، بغض النظر عن شكل مفتاحه الأساسي.
/// </summary>
public interface IHasUpdatedAt
{
    DateTime UpdatedAt { get; set; }
}
