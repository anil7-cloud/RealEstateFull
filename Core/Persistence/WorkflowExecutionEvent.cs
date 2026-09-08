namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string EventName { get; set; } = string.Empty;

    public string? PayloadJson { get; set; }

    public bool IsProcessed { get; set; }

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public DateTime? ProcessedAt { get; set; }

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
