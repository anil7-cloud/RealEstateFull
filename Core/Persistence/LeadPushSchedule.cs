namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushSchedule
{
    public int Id { get; set; }

    public int? LeadPushCampaignId { get; set; }

    public int? LeadPushProviderId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ScheduledAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public bool IsRecurring { get; set; }

    public string RecurrenceRule { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsCompleted { get; set; }

    public int TotalRecipients { get; set; }

    public int SentCount { get; set; }

    public int FailedCount { get; set; }

    public string Status { get; set; } = "Pending";

    public string TimeZone { get; set; } = "Europe/Istanbul";

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
