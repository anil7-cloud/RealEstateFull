namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionTimeline
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionId { get; set; }

    public string StageName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public TimeSpan? Duration { get; set; }

    public string? Notes { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecution? WorkflowExecution { get; set; }
}
