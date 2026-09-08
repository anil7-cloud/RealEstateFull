namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRetryLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRetryPolicyId { get; set; }

    public Guid WorkflowExecutionStepId { get; set; }

    public int RetryNumber { get; set; }

    public bool IsSuccessful { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

    public TimeSpan Duration { get; set; }

    public WorkflowExecutionRetryPolicy? WorkflowExecutionRetryPolicy { get; set; }

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
