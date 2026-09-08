namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertySummaryDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string PropertyType { get; set; } = string.Empty;
}
