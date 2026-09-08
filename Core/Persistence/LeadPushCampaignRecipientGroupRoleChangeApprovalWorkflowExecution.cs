namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecution
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowId { get; set; }

    public string ExecutionId { get; set; } = Guid.NewGuid().ToString();

    public string Status { get; set; } = "Running";

    public int CurrentStep { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? FinishedAt { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflow? Workflow { get; set; }
}
