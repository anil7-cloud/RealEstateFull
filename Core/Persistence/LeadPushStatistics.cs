namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushStatistics
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public int TotalSent { get; set; }

    public int TotalDelivered { get; set; }

    public int TotalOpened { get; set; }

    public int TotalClicked { get; set; }

    public int TotalFailed { get; set; }

    public decimal OpenRate { get; set; }

    public decimal ClickRate { get; set; }

    public DateTime LastCalculatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
