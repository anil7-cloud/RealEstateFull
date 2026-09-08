namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public bool IsSuccessful { get; set; }

    public string ResultCode { get; set; } = string.Empty;

    public string? ResultMessage { get; set; }

    public TimeSpan Duration { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime FinishedAt { get; set; }

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
