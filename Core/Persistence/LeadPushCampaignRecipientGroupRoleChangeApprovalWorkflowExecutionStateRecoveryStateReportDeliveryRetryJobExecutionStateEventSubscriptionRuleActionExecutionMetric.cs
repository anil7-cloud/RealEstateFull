namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionMetric
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public decimal MetricValue { get; set; }

    public string? MetricUnit { get; set; }

    public bool IsThresholdExceeded { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecution? ActionExecution { get; set; }
}
