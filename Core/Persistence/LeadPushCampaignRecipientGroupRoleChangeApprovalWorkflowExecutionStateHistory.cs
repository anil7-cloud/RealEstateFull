namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateId { get; set; }

    public string PreviousState { get; set; } = string.Empty;

    public string CurrentState { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? ChangeReason { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionState? ExecutionState { get; set; }
}
