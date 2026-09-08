namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowId { get; set; }

    public Guid UserId { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Action { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsCompleted { get; set; }

    public Guid? ParentId { get; set; }

    public WorkflowExecutionHistory? Parent { get; set; }
}
