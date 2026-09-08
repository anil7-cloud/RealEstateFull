namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowStep
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowId { get; set; }

    public int StepNumber { get; set; }

    public string StepName { get; set; } = string.Empty;

    public string Approver { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime? CompletedAt { get; set; }

    public string? Comments { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflow? Workflow { get; set; }
}
