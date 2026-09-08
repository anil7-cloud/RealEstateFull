namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryCheckpoint
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryId { get; set; }

    public string CheckpointName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsPassed { get; set; }

    public string? ValidationMessage { get; set; }

    public DateTime ReachedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecovery? Recovery { get; set; }
}
