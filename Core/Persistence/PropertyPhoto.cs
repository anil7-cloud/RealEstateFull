namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyPhoto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string Caption { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsCoverPhoto { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Eski servis uyumluluğu
    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }


    public PropertyListing? PropertyListing { get; set; }
}
