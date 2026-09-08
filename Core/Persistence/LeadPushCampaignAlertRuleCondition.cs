namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertRuleCondition
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertRuleId { get; set; }

    public string FieldName { get; set; } = string.Empty;

    public string Operator { get; set; } = string.Empty;

    public string ExpectedValue { get; set; } = string.Empty;

    public int Priority { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertRule? AlertRule { get; set; }
}
