namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushCampaignAlertRuleGroupResult
{
    public int Id { get; set; }

    public int LeadPushCampaignAlertRuleGroupExecutionId { get; set; }

    public int PassedRules { get; set; }

    public int FailedRules { get; set; }

    public decimal MatchPercentage { get; set; }

    public bool IsSuccessful { get; set; }

    public string? Summary { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushCampaignAlertRuleGroupExecution? RuleGroupExecution { get; set; }
}
