namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyImage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsCover { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;


    // Eski servislerle uyumluluk
    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }

    public string Url
    {
        get => ImageUrl;
        set => ImageUrl = value;
    }


    public PropertyListing? PropertyListing { get; set; }
}
