namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionConflict
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionMergeId { get; set; }

    public string ConflictKey { get; set; } = string.Empty;

    public string? SourceValue { get; set; }

    public string? TargetValue { get; set; }

    public string? ResolvedValue { get; set; }

    public bool IsResolved { get; set; }

    public Guid? ResolvedByUserId { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public WorkflowExecutionRevisionMerge? WorkflowExecutionRevisionMerge { get; set; }
}
