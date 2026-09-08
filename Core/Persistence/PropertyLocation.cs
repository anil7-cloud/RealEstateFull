namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyLocation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string Country { get; set; } = "Türkiye";

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string? Neighborhood { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string? MapAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
