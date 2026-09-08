namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyFilterRequestDto
{
    public string? City { get; set; }

    public string? District { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? PropertyTypeId { get; set; }

    public int? StatusId { get; set; }

    public int? RoomCount { get; set; }

    public int? BuildingAge { get; set; }

    public decimal? MinSquareMeter { get; set; }

    public decimal? MaxSquareMeter { get; set; }
}
