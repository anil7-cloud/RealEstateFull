namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyViewHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public Guid PropertyListingId { get; set; }

    public DateTime ViewedAt { get; set; } = DateTime.UtcNow;

    public string? DeviceInfo { get; set; }

    public string? IpAddress { get; set; }

    public PropertyListing? PropertyListing { get; set; }
}
