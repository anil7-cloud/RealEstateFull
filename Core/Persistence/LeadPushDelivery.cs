namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushDelivery
{
    public int Id { get; set; }

    public int LeadPushTaskId { get; set; }

    public string ProviderMessageId { get; set; } = string.Empty;

    public string Channel { get; set; } = "Push";

    public string Recipient { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime? SentAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public DateTime? OpenedAt { get; set; }

    public DateTime? ClickedAt { get; set; }

    public string FailureReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushTask? Task { get; set; }
}
