namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyTag
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string TagName { get; set; } = string.Empty;

    public string? TagValue { get; set; }

    public bool IsVisible { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
