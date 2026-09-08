namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushConversionAttribution
{
    public int Id { get; set; }

    public int LeadPushConversionId { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int LeadPushTaskId { get; set; }

    public string AttributionModel { get; set; } = "LastTouch";

    public decimal AttributionWeight { get; set; } = 1.0m;

    public DateTime AttributedAt { get; set; } = DateTime.UtcNow;

    public LeadPushConversion? Conversion { get; set; }

    public LeadPushCampaign? Campaign { get; set; }

    public LeadPushTask? Task { get; set; }
}
