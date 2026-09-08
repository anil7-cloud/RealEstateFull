namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class Property
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsPremium { get; set; }

    public bool IsActive { get; set; } = true;

    public int ViewCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int AppUserId { get; set; }

    public string PropertyType { get; set; } = "Satılık";

    public string RoomCount { get; set; } = "";
}
