namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyDetailRequestDto
{
    public int PropertyId { get; set; }

    public string Description { get; set; } = string.Empty;

    public int RoomCount { get; set; }

    public int BathroomCount { get; set; }

    public decimal SquareMeter { get; set; }

    public int BuildingAge { get; set; }

    public int Floor { get; set; }

    public int TotalFloor { get; set; }
}
