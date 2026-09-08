namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionSnapshotRevisionRevisionHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionSnapshotRevisionRevisionId { get; set; }

    public Guid? PerformedByUserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionSnapshotRevisionRevision? WorkflowExecutionRevisionSnapshotRevisionRevision { get; set; }
}
