namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionState
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public string State { get; set; } = "Draft";

    public string? StateDataJson { get; set; }

    public bool IsActive { get; set; } = true;

    public Guid? ChangedByUserId { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }
}
