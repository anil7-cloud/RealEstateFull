namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiAutomationResultDto
{
    public int PropertyId { get; set; }

    public string AutomationType { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public string Result { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
