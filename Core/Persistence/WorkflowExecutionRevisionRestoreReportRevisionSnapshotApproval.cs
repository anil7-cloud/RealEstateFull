namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportRevisionSnapshotApproval
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportRevisionSnapshotId { get; set; }

    public Guid ApproverUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public string? ApprovalComment { get; set; }

    public bool IsFinalApproval { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestoreReportRevisionSnapshot? WorkflowExecutionRevisionRestoreReportRevisionSnapshot { get; set; }
}
