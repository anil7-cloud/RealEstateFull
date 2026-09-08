namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyContract
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public Guid SellerUserId { get; set; }

    public Guid BuyerUserId { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public decimal ContractAmount { get; set; }

    public string Status { get; set; } = "Draft";

    public DateTime SignedDate { get; set; }

    public string? ContractFileUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyListing? PropertyListing { get; set; }

    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }

}

