namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionMetric
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public decimal MetricValue { get; set; }

    public string? Unit { get; set; }

    public decimal? TargetValue { get; set; }

    public bool IsThresholdExceeded { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecution? PolicyExecution { get; set; }
}
