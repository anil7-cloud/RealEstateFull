namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionResult
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionId { get; set; }

    public bool IsSuccessful { get; set; }

    public int TotalSteps { get; set; }

    public int SuccessfulSteps { get; set; }

    public int FailedSteps { get; set; }

    public string? Summary { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecution? WorkflowExecution { get; set; }
}
