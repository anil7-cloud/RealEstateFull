namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyFeature
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string FeatureName { get; set; } = string.Empty;

    public string? FeatureValue { get; set; }

    public bool IsImportant { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
