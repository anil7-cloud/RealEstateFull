namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyWorkflowLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyWorkflowId { get; set; }

    public Guid? UserId { get; set; }

    public string Level { get; set; } = "Info";

    public string Message { get; set; } = string.Empty;

    public string? DataJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyWorkflow? PropertyWorkflow { get; set; }
}
