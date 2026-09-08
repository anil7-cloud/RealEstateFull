namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReportRevisionVersion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreReportRevisionId { get; set; }

    public string Version { get; set; } = "1.0";

    public string? ChangeLog { get; set; }

    public bool IsCurrent { get; set; } = true;

    public Guid? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestoreReportRevision? WorkflowExecutionRevisionRestoreReportRevision { get; set; }
}
