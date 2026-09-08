namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyLevel
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyId { get; set; }

    public int Level { get; set; }

    public int DelayMinutes { get; set; }

    public string NotificationChannel { get; set; } = string.Empty;

    public string Recipient { get; set; } = string.Empty;

    public bool AutoResolve { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicy? EscalationPolicy { get; set; }
}
