namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyListing
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OwnerUserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PropertyType { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Currency { get; set; } = "TRY";

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public double Area { get; set; }

    public int RoomCount { get; set; }

    public int BathroomCount { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
