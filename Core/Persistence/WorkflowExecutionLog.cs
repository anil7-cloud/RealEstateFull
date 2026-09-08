namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionStepId { get; set; }

    public string Level { get; set; } = "Information";

    public string Message { get; set; } = string.Empty;

    public string? Exception { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? UserId { get; set; }

    public WorkflowExecutionStep? WorkflowExecutionStep { get; set; }
}
