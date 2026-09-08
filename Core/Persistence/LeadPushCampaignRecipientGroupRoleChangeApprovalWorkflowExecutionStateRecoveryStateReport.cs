namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReport
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateId { get; set; }

    public string ReportTitle { get; set; } = string.Empty;

    public string ReportType { get; set; } = string.Empty;

    public string? Summary { get; set; }

    public bool IsFinal { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public string? GeneratedBy { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryState? RecoveryState { get; set; }
}
