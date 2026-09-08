namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyKeyword
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string Keyword { get; set; } = string.Empty;

    public int SearchWeight { get; set; } = 1;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }
}
