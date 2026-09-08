namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertId { get; set; }

    public string PreviousStatus { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = string.Empty;

    public string? ChangedReason { get; set; }

    public string? ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlert? Alert { get; set; }
}
