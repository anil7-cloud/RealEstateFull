namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAmenityRequestDto
{
    public int PropertyId { get; set; }

    public string AmenityName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;
}
