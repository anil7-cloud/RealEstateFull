namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportRevisionSnapshot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportRevisionId { get; set; }

    public string SnapshotName { get; set; } = string.Empty;

    public string SnapshotDataJson { get; set; } = "{}";

    public string? Description { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestoreReportRevision? WorkflowExecutionRevisionRestoreReportRevision { get; set; }
}
