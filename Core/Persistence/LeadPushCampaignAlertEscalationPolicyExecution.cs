namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecution
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyId { get; set; }

    public int LeadPushCampaignAlertId { get; set; }

    public int CurrentLevel { get; set; }

    public bool IsCompleted { get; set; }

    public string Status { get; set; } = "Running";

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public LeadPushCampaignAlertEscalationPolicy? EscalationPolicy { get; set; }

    public LeadPushCampaignAlert? Alert { get; set; }
}
