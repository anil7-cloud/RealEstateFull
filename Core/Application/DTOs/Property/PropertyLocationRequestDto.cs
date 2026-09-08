namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyLocationRequestDto
{
    public int PropertyId { get; set; }

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}
