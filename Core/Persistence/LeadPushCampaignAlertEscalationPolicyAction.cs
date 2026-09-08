namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyAction
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyLevelId { get; set; }

    public string ActionName { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string? ActionValue { get; set; }

    public int ExecutionOrder { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyLevel? PolicyLevel { get; set; }
}
