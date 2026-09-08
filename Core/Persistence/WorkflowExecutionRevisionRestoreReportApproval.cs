namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportApproval
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportId { get; set; }

    public Guid ApproverUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public string? ApprovalComment { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public bool IsFinalApproval { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestoreReport? WorkflowExecutionRevisionRestoreReport { get; set; }
}
