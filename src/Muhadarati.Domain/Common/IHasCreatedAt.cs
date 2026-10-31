namespace Muhadarati.Domain.Common;

/// <summary>
/// يطبَّق على أي كيان يحتاج طابع وقت إنشاء تلقائي (UTC)، بغض النظر عن شكل مفتاحه الأساسي.
/// </summary>
public interface IHasCreatedAt
{
    DateTime CreatedAt { get; set; }
}
