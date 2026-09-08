namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyCommissionRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public decimal CommissionAmount { get; set; }

    public decimal CommissionRate { get; set; }

    public string CommissionType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
