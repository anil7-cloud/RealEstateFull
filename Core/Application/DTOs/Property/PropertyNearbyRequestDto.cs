namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyNearbyRequestDto
{
    public int PropertyId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int RadiusKm { get; set; }

    public string? Category { get; set; }
}
