namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientGroup
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string GroupName { get; set; } = string.Empty;

    public string? GroupCode { get; set; }

    public string? Description { get; set; }

    public bool IsPrimaryGroup { get; set; }

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LeftAt { get; set; }

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
