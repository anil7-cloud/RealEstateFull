namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlert
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string AlertType { get; set; } = string.Empty;

    public string Severity { get; set; } = "Info";

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ResolvedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
