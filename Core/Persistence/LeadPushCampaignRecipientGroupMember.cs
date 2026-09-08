namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroupMember
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientGroupId { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string Role { get; set; } = "Member";

    public bool IsActive { get; set; } = true;

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LeftAt { get; set; }

    public LeadPushCampaignRecipientGroup? Group { get; set; }

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
