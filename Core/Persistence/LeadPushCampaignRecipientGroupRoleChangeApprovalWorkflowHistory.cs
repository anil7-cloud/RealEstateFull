namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowId { get; set; }

    public string PreviousStatus { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = string.Empty;

    public int StepNumber { get; set; }

    public string? ChangedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflow? Workflow { get; set; }
}
