namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionSnapshot
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public Guid WorkflowExecutionStepId { get; set; }

    public string SnapshotName { get; set; } = string.Empty;

    public string SnapshotDataJson { get; set; } = "{}";

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedByUserId { get; set; }

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
