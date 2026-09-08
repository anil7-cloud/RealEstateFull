namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushRule
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ConditionExpression { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string ActionData { get; set; } = string.Empty;

    public int Priority { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
