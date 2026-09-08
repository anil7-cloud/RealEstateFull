namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionParameter
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid WorkflowExecutionHistoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string DataType { get; set; } = "string";

    public bool IsRequired { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public WorkflowExecutionHistory? WorkflowExecutionHistory { get; set; }
}
