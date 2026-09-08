namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerValueDto
{
    public int UserId { get; set; }

    public decimal CustomerValue { get; set; }

    public string ValueLevel { get; set; } = string.Empty;

    public string ValueReason { get; set; } = string.Empty;

    public decimal LifetimeValue { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
