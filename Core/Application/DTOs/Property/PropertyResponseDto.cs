namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public int PropertyTypeId { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedAt { get; set; }
}
