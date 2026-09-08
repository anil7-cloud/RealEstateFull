namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionTransitionHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionTransitionId { get; set; }

    public string PreviousState { get; set; } = string.Empty;

    public string CurrentState { get; set; } = string.Empty;

    public Guid? ChangedByUserId { get; set; }

    public string? Comment { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionTransition? WorkflowExecutionRevisionTransition { get; set; }
}
