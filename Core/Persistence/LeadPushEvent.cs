namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushEvent
{
    public int Id { get; set; }

    public int LeadPushTaskId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string EventData { get; set; } = string.Empty;

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public string Source { get; set; } = string.Empty;

    public LeadPushTask? Task { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime EventTime { get; set; } = DateTime.UtcNow;
    public int? LeadPushCampaignId { get; set; }
    public int? LeadPushCampaignRecipientId { get; set; }
    public int? UserId { get; set; }
}
