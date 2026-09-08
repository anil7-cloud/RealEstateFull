namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupRoleChange
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupRoleId { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string PreviousRole { get; set; } = string.Empty;

    public string NewRole { get; set; } = string.Empty;

    public string? ChangedBy { get; set; }

    public string? Reason { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipientGroupRole? GroupRole { get; set; }

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
