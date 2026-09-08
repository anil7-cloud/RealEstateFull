namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionResolutionHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionResolutionId { get; set; }

    public Guid? ChangedByUserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionResolution? WorkflowExecutionRevisionResolution { get; set; }
}
