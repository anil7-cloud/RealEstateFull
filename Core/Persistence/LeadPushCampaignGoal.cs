namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignGoal
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string GoalType { get; set; } = string.Empty;

    public decimal TargetValue { get; set; }

    public decimal CurrentValue { get; set; }

    public decimal ProgressPercentage { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
