namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionStateEventSubscriber
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionStateEventId { get; set; }

    public string SubscriberName { get; set; } = string.Empty;

    public string SubscriberType { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool LastDeliverySucceeded { get; set; }

    public DateTime? LastDeliveredAt { get; set; }

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionStateEvent? Event { get; set; }
}
