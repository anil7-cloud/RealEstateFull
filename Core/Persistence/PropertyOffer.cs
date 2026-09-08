namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyOffer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyListingId { get; set; }

    public Guid CustomerUserId { get; set; }

    public decimal OfferAmount { get; set; }

    public string Status { get; set; } = "Pending";

    public string? Message { get; set; }

    public DateTime OfferDate { get; set; } = DateTime.UtcNow;

    public DateTime? ResponseDate { get; set; }

    public PropertyListing? PropertyListing { get; set; }

    public Guid PropertyId
    {
        get => PropertyListingId;
        set => PropertyListingId = value;
    }

    public Guid BuyerUserId
    {
        get => CustomerUserId;
        set => CustomerUserId = value;
    }

    public string? Note
    {
        get => Message;
        set => Message = value;
    }


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}


