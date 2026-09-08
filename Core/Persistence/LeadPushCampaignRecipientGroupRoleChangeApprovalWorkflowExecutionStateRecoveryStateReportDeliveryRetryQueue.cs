namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryQueue
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryPolicyId { get; set; }

    public int QueuePosition { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime ScheduledAt { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public int Priority { get; set; }

    public string? Notes { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryPolicy? RetryPolicy { get; set; }
}
