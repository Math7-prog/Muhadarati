namespace Muhadarati.Domain.Administration;

/// <summary>تجميع يومي لعدد طلبات ميزة ذكاء اصطناعي واحدة — مفتاح مركّب (FeatureKey, Date) بدون Id مستقل.</summary>
public class AiUsageStat
{
    public AiFeature FeatureKey { get; set; }
    public DateOnly Date { get; set; }

    public long RequestCount { get; set; }
}
