namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public string CronExpression { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;

    public DateTime? LastRunAt { get; set; }

    public DateTime? NextRunAt { get; set; }

    public int FailureCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
