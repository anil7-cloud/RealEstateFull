namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public string DocumentName { get; set; } = string.Empty;

    public string DocumentUrl { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public bool IsVerified { get; set; }

    public Guid? UploadedByUserId { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }

    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }

    public bool IsActive { get; set; } = true;

    public DateTime? ExpirationDate { get; set; }

    public DateTime UploadDate
    {
        get => UploadedAt;
        set => UploadedAt = value;
    }


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}


