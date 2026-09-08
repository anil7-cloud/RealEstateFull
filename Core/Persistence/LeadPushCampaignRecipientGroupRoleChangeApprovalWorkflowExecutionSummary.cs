namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionSummary
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionId { get; set; }

    public int TotalSteps { get; set; }

    public int CompletedSteps { get; set; }

    public int FailedSteps { get; set; }

    public int SkippedSteps { get; set; }

    public TimeSpan TotalDuration { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecution? WorkflowExecution { get; set; }
}
