namespace Core.Application.DTOs.PropertyMedia;

public class PropertyMediaDto
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string MediaUrl { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public string MediaType { get; set; } = "Image";

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPrimary { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
