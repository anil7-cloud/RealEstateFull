namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleHistory
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleId { get; set; }

    public string PreviousRoleName { get; set; } = string.Empty;

    public string CurrentRoleName { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? ChangeReason { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRole? GroupRole { get; set; }
}
