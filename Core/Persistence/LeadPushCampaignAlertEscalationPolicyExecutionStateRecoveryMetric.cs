namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryMetric
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertEscalationPolicyExecutionStateRecoveryId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public decimal MetricValue { get; set; }

    public string? Unit { get; set; }

    public decimal? Threshold { get; set; }

    public bool ThresholdExceeded { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertEscalationPolicyExecutionStateRecovery? Recovery { get; set; }
}
