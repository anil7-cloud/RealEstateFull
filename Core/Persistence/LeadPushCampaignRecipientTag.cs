namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipientTag
{
    public int Id { get; set; }

    public int LeadPushCampaignRecipientId { get; set; }

    public string TagName { get; set; } = string.Empty;

    public string? TagColor { get; set; }

    public string? Description { get; set; }

    public bool IsSystemTag { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignRecipient? Recipient { get; set; }
}
