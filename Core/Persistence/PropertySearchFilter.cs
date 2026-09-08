namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertySearchFilter
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public string? Keyword { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? PropertyType { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public double? MinArea { get; set; }

    public double? MaxArea { get; set; }

    public int? RoomCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
