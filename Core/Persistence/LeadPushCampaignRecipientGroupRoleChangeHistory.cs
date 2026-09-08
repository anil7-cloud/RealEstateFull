namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChangeHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleChangeId { get; set; }

    public string PreviousStatus { get; set; } = string.Empty;

    public string CurrentStatus { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? Notes { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRoleChange? RoleChange { get; set; }
}
