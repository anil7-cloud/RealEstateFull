namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryResult
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryId { get; set; }

    public bool IsSuccessful { get; set; }

    public int RetryCount { get; set; }

    public TimeSpan RecoveryDuration { get; set; }

    public string? FailureReason { get; set; }

    public string? Summary { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecovery? Recovery { get; set; }
}
