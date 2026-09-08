namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertRuleGroupMember
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertRuleGroupId { get; set; }

    public int LeadPushCampaignAlertRuleId { get; set; }

    public int SortOrder { get; set; }

    public bool IsRequired { get; set; } = true;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertRuleGroup? RuleGroup { get; set; }

    public LeadPushCampaignAlertRule? AlertRule { get; set; }
}
