namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionCheckpoint
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public Guid WorkflowExecutionStepId { get; set; }

    public string CheckpointName { get; set; } = string.Empty;

    public string? StateJson { get; set; }

    public bool IsRestorable { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedByUserId { get; set; }

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
