namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertySearchResultDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int PropertyTypeId { get; set; }

    public int StatusId { get; set; }
}
