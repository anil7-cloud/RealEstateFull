namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushRuleExecution
{
    public int Id { get; set; }

    public int LeadPushRuleId { get; set; }

    public int LeadPushTaskId { get; set; }

    public bool IsSuccessful { get; set; }

    public string Result { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public TimeSpan ExecutionTime { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public LeadPushRule? Rule { get; set; }

    public LeadPushTask? Task { get; set; }
}
