namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateSnapshot
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateId { get; set; }

    public string StateName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public int RetryCount { get; set; }

    public bool IsRecoverable { get; set; }

    public string? SnapshotData { get; set; }

    public DateTime CapturedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionState? ExecutionState { get; set; }
}
