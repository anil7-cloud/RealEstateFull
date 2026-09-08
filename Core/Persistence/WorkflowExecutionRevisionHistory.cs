namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public Guid WorkflowExecutionHistoryId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid? PerformedByUserId { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
