namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionStatus
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public string Status { get; set; } = "Draft";

    public string? Reason { get; set; }

    public bool IsCurrent { get; set; } = true;

    public Guid? UpdatedByUserId { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }
}
