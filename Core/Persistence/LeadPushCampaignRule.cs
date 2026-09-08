namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignRule
{
    public int Id { get; set; }

    public int LeadPushCampaignId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string RuleType { get; set; } = string.Empty;

    public string RuleValue { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int Priority { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public LeadPushCampaign? Campaign { get; set; }
}
