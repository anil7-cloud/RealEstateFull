namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateId { get; set; }

    public string StateName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? ChangeReason { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryState? RecoveryState { get; set; }
}
