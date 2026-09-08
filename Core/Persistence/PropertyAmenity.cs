namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyAmenity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string AmenityName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsAvailable { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }

    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }

    public string Name
    {
        get => AmenityName;
        set => AmenityName = value;
    }

    public string? Category { get; set; }

}

