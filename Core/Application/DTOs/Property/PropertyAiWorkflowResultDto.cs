namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiWorkflowResultDto
{
    public int PropertyId { get; set; }

    public string WorkflowType { get; set; } = string.Empty;

    public string TriggerAction { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
