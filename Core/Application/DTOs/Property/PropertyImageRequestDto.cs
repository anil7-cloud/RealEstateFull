namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyImageRequestDto
{
    public int PropertyId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsMainImage { get; set; }
}
