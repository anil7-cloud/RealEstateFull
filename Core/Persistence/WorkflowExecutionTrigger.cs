namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionTrigger
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public string TriggerType { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;

    public DateTime? LastTriggeredAt { get; set; }

    public DateTime? NextTriggerAt { get; set; }

    public string? ConfigurationJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
