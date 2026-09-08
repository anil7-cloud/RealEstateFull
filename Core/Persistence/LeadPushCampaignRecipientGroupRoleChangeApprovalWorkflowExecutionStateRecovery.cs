namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecovery
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateId { get; set; }

    public string RecoveryStrategy { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }

    public int RetryAttempt { get; set; }

    public string? ErrorMessage { get; set; }

    public string? RecoveryDetails { get; set; }

    public DateTime RecoveredAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionState? ExecutionState { get; set; }
}
