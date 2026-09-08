namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportRevisionSnapshotReviewer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportRevisionSnapshotId { get; set; }

    public Guid ReviewerUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public string? ReviewComment { get; set; }

    public bool IsRequired { get; set; } = true;

    public DateTime? ReviewedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestoreReportRevisionSnapshot? WorkflowExecutionRevisionRestoreReportRevisionSnapshot { get; set; }
}
