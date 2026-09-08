namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertRuleExecution
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertRuleId { get; set; }

    public bool ConditionMatched { get; set; }

    public decimal ActualValue { get; set; }

    public decimal ThresholdValue { get; set; }

    public bool AlertCreated { get; set; }

    public string? Notes { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertRule? AlertRule { get; set; }
}
