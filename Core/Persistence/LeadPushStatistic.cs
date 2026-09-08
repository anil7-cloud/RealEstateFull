namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushStatistic
{
    public int Id { get; set; }

    public int? LeadPushCampaignId { get; set; }

    public DateTime StatisticDate { get; set; } = DateTime.UtcNow.Date;

    public int TotalSent { get; set; }

    public int TotalDelivered { get; set; }

    public int TotalOpened { get; set; }

    public int TotalClicked { get; set; }

    public int TotalFailed { get; set; }

    public decimal DeliveryRate { get; set; }

    public decimal OpenRate { get; set; }

    public decimal ClickRate { get; set; }

    public decimal FailureRate { get; set; }

    public int AndroidCount { get; set; }

    public int IOSCount { get; set; }

    public int WebCount { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
