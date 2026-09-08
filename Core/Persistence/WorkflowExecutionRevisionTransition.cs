namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionTransition
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public string FromState { get; set; } = string.Empty;

    public string ToState { get; set; } = string.Empty;

    public Guid? ChangedByUserId { get; set; }

    public string? Reason { get; set; }

    public DateTime TransitionedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }
}
