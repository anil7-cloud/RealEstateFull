namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionCondition
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionStepId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Expression { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;

    public bool LastEvaluationResult { get; set; }

    public DateTime? LastEvaluatedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
