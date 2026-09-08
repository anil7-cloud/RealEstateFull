namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertRuleGroupExecution
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertRuleGroupId { get; set; }

    public bool IsMatched { get; set; }

    public int MatchedRuleCount { get; set; }

    public int TotalRuleCount { get; set; }

    public bool AlertTriggered { get; set; }

    public string? ExecutionDetails { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertRuleGroup? RuleGroup { get; set; }
}
