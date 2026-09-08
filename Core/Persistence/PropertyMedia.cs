using System.ComponentModel.DataAnnotations.Schema;

namespace REAL_ESTATE_CLEAN.Core.Persistence;

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

    [NotMapped]
    public string Url
    {
        get => MediaUrl;
        set => MediaUrl = value;
    }

    public string? OriginalFileName { get; set; }

    public string? ContentType { get; set; }

    public long SizeBytes { get; set; }

    [NotMapped]
    public bool IsCover
    {
        get => IsPrimary;
        set => IsPrimary = value;
    }
}
