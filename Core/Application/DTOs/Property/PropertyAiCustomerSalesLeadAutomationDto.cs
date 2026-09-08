namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadAutomationDto
{
    public int UserId { get; set; }

    public string AutomationType { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public string Result { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
