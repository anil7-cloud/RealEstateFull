namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateSummary
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateId { get; set; }

    public int TotalTransitions { get; set; }

    public int SuccessfulTransitions { get; set; }

    public int FailedTransitions { get; set; }

    public TimeSpan TotalDuration { get; set; }

    public string CurrentStatus { get; set; } = string.Empty;

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryState? RecoveryState { get; set; }
}
