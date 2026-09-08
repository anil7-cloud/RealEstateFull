namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreReport
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreId { get; set; }

    public string ReportName { get; set; } = string.Empty;

    public string ReportDataJson { get; set; } = "{}";

    public string? Summary { get; set; }

    public Guid? GeneratedByUserId { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestore? WorkflowExecutionRevisionRestore { get; set; }
}
