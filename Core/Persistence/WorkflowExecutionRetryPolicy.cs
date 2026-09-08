namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRetryPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionStepId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int MaxRetryCount { get; set; }

    public int RetryIntervalSeconds { get; set; }

    public bool UseExponentialBackoff { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
