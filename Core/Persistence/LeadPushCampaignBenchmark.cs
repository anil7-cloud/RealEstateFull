namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignBenchmark
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string BenchmarkName { get; set; } = string.Empty;

    public decimal CampaignValue { get; set; }

    public decimal IndustryAverage { get; set; }

    public decimal BestValue { get; set; }

    public decimal DifferencePercentage { get; set; }

    public string Status { get; set; } = "Average";

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
