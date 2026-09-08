namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalStep
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalId { get; set; }

    public int StepOrder { get; set; }

    public string Approver { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public string? Comments { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApproval? Approval { get; set; }
}
