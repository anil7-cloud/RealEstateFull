namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportRevision
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReportId { get; set; }

    public int RevisionNumber { get; set; }

    public string RevisionSummary { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public DateTime RevisedAt { get; set; } = DateTime.UtcNow;

    public bool IsPublished { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryStateReport? Report { get; set; }
}
