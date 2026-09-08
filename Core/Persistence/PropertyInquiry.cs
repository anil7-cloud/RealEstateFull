namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyInquiry
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public Guid CustomerUserId { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Status { get; set; } = "New";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RespondedAt { get; set; }

    public PropertyListing? PropertyListing { get; set; }
}
