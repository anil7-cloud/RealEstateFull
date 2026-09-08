namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateSnapshot
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateId { get; set; }

    public string SnapshotName { get; set; } = string.Empty;

    public string StateName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public bool IsCurrent { get; set; }

    public string? SerializedState { get; set; }

    public DateTime CapturedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionState? ExecutionState { get; set; }
}
