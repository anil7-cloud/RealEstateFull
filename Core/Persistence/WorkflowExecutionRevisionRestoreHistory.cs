namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreId { get; set; }

    public Guid? PerformedByUserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? Details { get; set; }

    public bool IsSuccessful { get; set; }

    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestore? WorkflowExecutionRevisionRestore { get; set; }
}
