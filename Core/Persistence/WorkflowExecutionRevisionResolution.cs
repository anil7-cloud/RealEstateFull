namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionResolution
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionConflictId { get; set; }

    public Guid? ResolvedByUserId { get; set; }

    public string ResolutionType { get; set; } = string.Empty;

    public string? ResolutionNotes { get; set; }

    public string? FinalValue { get; set; }

    public DateTime ResolvedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevisionConflict? WorkflowExecutionRevisionConflict { get; set; }
}
