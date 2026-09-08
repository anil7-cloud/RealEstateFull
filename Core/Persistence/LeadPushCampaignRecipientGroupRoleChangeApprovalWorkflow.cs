namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflow
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalId { get; set; }

    public string WorkflowName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public int CurrentStep { get; set; }

    public int TotalSteps { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApproval? Approval { get; set; }
}
