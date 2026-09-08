namespace Core.Application.DTOs.PropertyMedia;

public class UpdatePropertyMediaDto
{
    public string MediaUrl { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public string MediaType { get; set; } = "Image";

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPrimary { get; set; }

    public bool IsActive { get; set; } = true;
}
