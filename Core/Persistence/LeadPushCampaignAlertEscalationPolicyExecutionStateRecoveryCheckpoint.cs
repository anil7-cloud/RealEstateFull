namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryCheckpoint
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryId { get; set; }

    public string CheckpointName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsPassed { get; set; }

    public string? ValidationMessage { get; set; }

    public DateTime ReachedAt { get; set; }

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecovery? Recovery { get; set; }
}
