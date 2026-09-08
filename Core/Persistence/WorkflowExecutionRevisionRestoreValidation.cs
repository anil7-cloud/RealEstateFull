namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionRestoreValidation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionRestoreId { get; set; }

    public string ValidationRule { get; set; } = string.Empty;

    public bool IsPassed { get; set; }

    public string? ValidationMessage { get; set; }

    public Guid? ValidatedByUserId { get; set; }

    public DateTime ValidatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionRestore? WorkflowExecutionRevisionRestore { get; set; }
}
