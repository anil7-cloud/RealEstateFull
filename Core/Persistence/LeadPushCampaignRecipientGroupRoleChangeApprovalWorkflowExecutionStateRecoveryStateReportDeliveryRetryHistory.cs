namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryPolicyId { get; set; }

    public int RetryNumber { get; set; }

    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

    public bool IsSuccessful { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public TimeSpan Duration { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryPolicy? RetryPolicy { get; set; }
}
