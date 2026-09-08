namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicy
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string PolicyName { get; set; } = string.Empty;

    public int InitialLevel { get; set; }

    public int MaxLevel { get; set; }

    public int EscalationIntervalMinutes { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
