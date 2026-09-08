namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyMapRequestDto
{
    public int PropertyId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int ZoomLevel { get; set; }

    public string MapProvider { get; set; } = string.Empty;
}
