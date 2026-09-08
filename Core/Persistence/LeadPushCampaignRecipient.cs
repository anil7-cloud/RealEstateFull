namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRecipient
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int LeadId { get; set; }

    public string Recipient { get; set; } = string.Empty;

    public string Channel { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public bool IsSent { get; set; }

    public DateTime? SentAt { get; set; }

    public bool IsDelivered { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public bool IsOpened { get; set; }

    public DateTime? OpenedAt { get; set; }

    public bool IsClicked { get; set; }

    public DateTime? ClickedAt { get; set; }

    public bool IsConverted { get; set; }

    public DateTime? ConvertedAt { get; set; }

    public string? ProviderMessageId { get; set; }

    public string? ProviderResponse { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
