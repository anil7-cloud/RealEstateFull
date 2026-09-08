namespace REAL_ESTATE_CLEAN.Core.Application.DTO;

public class PropertyDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsPremium { get; set; }

    public bool IsActive { get; set; }

    public int ViewCount { get; set; }

    public string? CoverImageUrl { get; set; }

    public string PropertyType { get; set; } = string.Empty;

    public string RoomCount { get; set; } = string.Empty;
}
