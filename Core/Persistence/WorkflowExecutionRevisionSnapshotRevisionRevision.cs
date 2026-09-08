namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionSnapshotRevisionRevision
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionSnapshotRevisionVersionId { get; set; }

    public int RevisionNumber { get; set; }

    public string? Description { get; set; }

    public string RevisionDataJson { get; set; } = "{}";

    public Guid? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionSnapshotRevisionVersion? WorkflowExecutionRevisionSnapshotRevisionVersion { get; set; }
}
