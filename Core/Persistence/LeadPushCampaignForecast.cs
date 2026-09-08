namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignForecast
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public DateTime ForecastDate { get; set; }

    public int ExpectedRecipients { get; set; }

    public decimal ExpectedOpenRate { get; set; }

    public decimal ExpectedClickRate { get; set; }

    public decimal ExpectedConversionRate { get; set; }

    public decimal EstimatedRevenue { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
