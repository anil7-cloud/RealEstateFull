namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportReviewer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportId { get; set; }

    public Guid ReviewerUserId { get; set; }

    public string Status { get; set; } = "Pending";

    public string? ReviewComment { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public bool IsRequired { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestoreReport? WorkflowExecutionRevisionRestoreReport { get; set; }
}
