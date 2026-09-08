namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionState
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecutionId { get; set; }

    public string StateName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsCurrent { get; set; }

    public DateTime EnteredAt { get; set; }

    public DateTime? ExitedAt { get; set; }

    public string? Remarks { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateEventSubscriptionRuleActionExecution? ActionExecution { get; set; }
}
