namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryState
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecoveryId { get; set; }

    public string StateName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsActive { get; set; }

    public bool IsTerminal { get; set; }

    public string? Description { get; set; }

    public DateTime EnteredAt { get; set; } = DateTime.UtcNow;

    public DateTime? ExitedAt { get; set; }

    public LeadPushCampaignRecipientGroupRoleChangeApprovalWorkflowExecutionStateRecovery? Recovery { get; set; }
}
