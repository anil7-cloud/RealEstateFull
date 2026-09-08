namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class VirtualTour
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string Provider { get; set; } = string.Empty;

    public string TourUrl { get; set; } = string.Empty;

    public string ThumbnailUrl { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
