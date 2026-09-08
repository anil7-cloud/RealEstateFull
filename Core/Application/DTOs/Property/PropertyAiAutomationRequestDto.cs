namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiAutomationRequestDto
{
    public int PropertyId { get; set; }

    public string AutomationType { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
