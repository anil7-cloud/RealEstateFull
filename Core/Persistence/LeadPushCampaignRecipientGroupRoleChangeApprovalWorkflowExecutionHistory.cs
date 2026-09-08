namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionId { get; set; }

    public int StepNumber { get; set; }

    public string PreviousStatus { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecution? WorkflowExecution { get; set; }
}
