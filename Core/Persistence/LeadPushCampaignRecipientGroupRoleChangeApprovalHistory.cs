namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApprovalHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeApprovalId { get; set; }

    public string PreviousStatus { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? Comments { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChangeApproval? Approval { get; set; }
}
