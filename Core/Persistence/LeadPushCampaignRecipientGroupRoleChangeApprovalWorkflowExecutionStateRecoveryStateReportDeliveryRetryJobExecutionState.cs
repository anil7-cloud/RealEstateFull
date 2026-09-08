namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionState
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionId { get; set; }

    public string StateName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsCurrent { get; set; }

    public DateTime EnteredAt { get; set; }

    public DateTime? ExitedAt { get; set; }

    public string? Remarks { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecution? Execution { get; set; }
}
