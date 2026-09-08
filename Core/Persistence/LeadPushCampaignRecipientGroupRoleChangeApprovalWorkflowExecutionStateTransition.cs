namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateTransition
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateId { get; set; }

    public string FromState { get; set; } = string.Empty;

    public string ToState { get; set; } = string.Empty;

    public string Trigger { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime TransitionedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionState? ExecutionState { get; set; }
}
