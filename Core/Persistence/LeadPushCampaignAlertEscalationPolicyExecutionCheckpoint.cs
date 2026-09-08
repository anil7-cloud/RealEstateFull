namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionCheckpoint
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionId { get; set; }

    public string CheckpointName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsPassed { get; set; }

    public DateTime ReachedAt { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecution? PolicyExecution { get; set; }
}
