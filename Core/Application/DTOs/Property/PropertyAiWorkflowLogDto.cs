namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiWorkflowLogDto
{
    public int PropertyId { get; set; }

    public string WorkflowType { get; set; } = string.Empty;

    public string TriggerAction { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
