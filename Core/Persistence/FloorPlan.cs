namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class FloorPlan
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public decimal AreaSquareMeters { get; set; }

    public int FloorNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
