namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyRevenueRequestDto
{
    public int PropertyId { get; set; }

    public decimal RevenueAmount { get; set; }

    public string RevenueType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime RevenueDate { get; set; } = DateTime.UtcNow;

    public int? CreatedByUserId { get; set; }
}
