namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiAutomationHistoryDto
{
    public int PropertyId { get; set; }

    public string AutomationType { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
}
