using REAL_ESTATE_CLEAN.Core.Persistence;

namespace Core.Domain.Entities;
public class PropertyMedia
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

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Property? Property { get; set; }
}
