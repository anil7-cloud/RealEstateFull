namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionSnapshotRevisionReviewer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionSnapshotRevisionId { get; set; }

    public Guid ReviewerUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public string? ReviewComment { get; set; }

    public bool IsRequired { get; set; } = true;

    public DateTime? ReviewedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionSnapshotRevision? WorkflowExecutionRevisionSnapshotRevision { get; set; }
}
