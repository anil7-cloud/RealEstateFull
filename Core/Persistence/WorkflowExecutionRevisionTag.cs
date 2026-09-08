namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionRevisionTag
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionRevisionId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Color { get; set; }

    public string? Description { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionRevision? WorkflowExecutionRevision { get; set; }
}
