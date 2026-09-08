namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionEventQueue
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionEventId { get; set; }

    public string QueueName { get; set; } = string.Empty;

    public int Priority { get; set; }

    public string Status { get; set; } = "Pending";

    public int RetryCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ProcessedAt { get; set; }

    public WorkflowExecutionEvent? WorkflowExecutionEvent { get; set; }
}
