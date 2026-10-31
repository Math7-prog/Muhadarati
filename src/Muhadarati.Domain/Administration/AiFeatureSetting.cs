using Muhadarati.Domain.Common;

namespace Muhadarati.Domain.Administration;

/// <summary>إعداد المزود والنموذج لميزة ذكاء اصطناعي واحدة — صف ثابت لكل ميزة، FeatureKey هو المفتاح الأساسي.</summary>
public class AiFeatureSetting : IHasCreatedAt, IHasUpdatedAt
{
    public AiFeature FeatureKey { get; set; }

    public string Provider { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;

    /// <summary>تفعيل "نموذج محلي فقط" لهذي الميزة.</summary>
    public bool LocalOnly { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
