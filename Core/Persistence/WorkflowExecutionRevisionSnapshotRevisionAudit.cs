namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionSnapshotRevisionAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionSnapshotRevisionId { get; set; }

    public Guid? UserId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionSnapshotRevision? WorkflowExecutionRevisionSnapshotRevision { get; set; }
}
