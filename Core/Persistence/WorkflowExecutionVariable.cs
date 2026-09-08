namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionVariable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public string Key { get; set; } = string.Empty;

    public string? Value { get; set; }

    public string DataType { get; set; } = "string";

    public bool IsSecret { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
