namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public Guid CustomerUserId { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }

    public PropertyListing? PropertyListing { get; set; }
}
