namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class WorkflowExecutionTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Version { get; set; } = "1.0";

    public bool IsActive { get; set; } = true;

    public string DefinitionJson { get; set; } = "{}";

    public Guid? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
