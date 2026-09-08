namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignOptimization
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string OptimizationType { get; set; } = string.Empty;

    public string CurrentValue { get; set; } = string.Empty;

    public string SuggestedValue { get; set; } = string.Empty;

    public decimal ExpectedImprovement { get; set; }

    public bool IsApplied { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? AppliedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
