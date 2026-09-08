namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportVersion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportId { get; set; }

    public string Version { get; set; } = "1.0";

    public string? ChangeLog { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsCurrent { get; set; } = true;

    public WorkflowExecutionRevisionRestoreReport? WorkflowExecutionRevisionRestoreReport { get; set; }
}
