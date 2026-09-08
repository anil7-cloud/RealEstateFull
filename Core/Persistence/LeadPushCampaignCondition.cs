namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignCondition
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string Field { get; set; } = string.Empty;

    public string Operator { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string LogicalOperator { get; set; } = "AND";

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaign? Campaign { get; set; }
}
