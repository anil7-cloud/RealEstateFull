namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriber
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventId { get; set; }

    public string SubscriberName { get; set; } = string.Empty;

    public string SubscriberType { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool LastDeliverySucceeded { get; set; }

    public DateTime? LastDeliveredAt { get; set; }

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEvent? Event { get; set; }
}
