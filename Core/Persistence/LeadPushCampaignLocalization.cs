namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignLocalization
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string LanguageCode { get; set; } = "tr";

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string ActionUrl { get; set; } = string.Empty;

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
