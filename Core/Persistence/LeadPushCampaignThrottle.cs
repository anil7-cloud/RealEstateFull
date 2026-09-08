namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignThrottle
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int MaxMessagesPerMinute { get; set; }

    public int MaxMessagesPerHour { get; set; }

    public int MaxConcurrentJobs { get; set; }

    public bool StopOnProviderError { get; set; } = true;

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
