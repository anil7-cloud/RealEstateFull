namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateAudit
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionStateId { get; set; }

    public string AuditAction { get; set; } = string.Empty;

    public string? PreviousState { get; set; }

    public string? CurrentState { get; set; }

    public string? PerformedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime AuditedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportDeliveryRetryJobExecutionState? ExecutionState { get; set; }
}
