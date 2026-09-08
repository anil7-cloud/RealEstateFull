namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionAction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionStepId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string? ConfigurationJson { get; set; }

    public bool IsEnabled { get; set; } = true;

    public int ExecutionOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
