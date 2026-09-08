namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadAutomationRule
{
    public int Id { get; set; }

    public string RuleName { get; set; } = string.Empty;

    public string TriggerEvent { get; set; } = string.Empty;

    public string ConditionExpression { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string ActionValue { get; set; } = string.Empty;

    public int Priority { get; set; }

    public bool IsEnabled { get; set; } = true;

    public bool RunOnlyOnce { get; set; }

    public DateTime? LastExecutedAt { get; set; }

    public int ExecutionCount { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
