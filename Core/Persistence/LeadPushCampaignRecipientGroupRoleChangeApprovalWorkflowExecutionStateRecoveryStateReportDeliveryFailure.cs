namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryFailure
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryAttemptId { get; set; }

    public string FailureCode { get; set; } = string.Empty;

    public string FailureReason { get; set; } = string.Empty;

    public bool IsRetryable { get; set; }

    public int RetryCount { get; set; }

    public DateTime FailedAt { get; set; } = DateTime.UtcNow;

    public DateTime? NextRetryAt { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryAttempt? DeliveryAttempt { get; set; }
}
