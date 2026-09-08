namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertRuleGroup
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LogicalOperator { get; set; } = "AND";

    public bool IsEnabled { get; set; } = true;

    public int Priority { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
