namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyPriceHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public decimal Difference { get; set; }

    public decimal PercentageChange { get; set; }

    public string? ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;


    // Eski servis uyumluluğu
    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }
}
