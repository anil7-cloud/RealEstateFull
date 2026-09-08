namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionMerge
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid SourceRevisionId { get; set; }

    public Guid TargetRevisionId { get; set; }

    public Guid? MergedByUserId { get; set; }

    public string? MergeStrategy { get; set; }

    public string? MergeNotes { get; set; }

    public bool HasConflicts { get; set; }

    public DateTime MergedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevision? SourceRevision { get; set; }

    public WorkflowExecutionRevision? TargetRevision { get; set; }
}
