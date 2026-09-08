namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionAudit
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionId { get; set; }

    public string AuditAction { get; set; } = string.Empty;

    public string? PreviousStatus { get; set; }

    public string? CurrentStatus { get; set; }

    public string? PerformedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime AuditedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecution? Execution { get; set; }
}
