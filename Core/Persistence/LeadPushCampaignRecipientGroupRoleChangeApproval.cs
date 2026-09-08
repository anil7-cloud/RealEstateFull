namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeApproval
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeId { get; set; }

    public string Approver { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public string? Comments { get; set; }

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ApprovedAt { get; set; }

    public bool IsFinalApproval { get; set; }

    public LeadPushCampaignRecipientGroupRoleChange? RoleChange { get; set; }
}
