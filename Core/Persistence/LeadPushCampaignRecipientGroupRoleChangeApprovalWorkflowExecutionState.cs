namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionState
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionId { get; set; }

    public string PreviousState { get; set; } = string.Empty;

    public string CurrentState { get; set; } = string.Empty;

    public string? TransitionReason { get; set; }

    public bool IsFinalState { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecution? WorkflowExecution { get; set; }
}
