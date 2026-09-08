namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionComment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public Guid UserId { get; set; }

    public string Comment { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }
}
