namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadOffer
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public decimal OfferAmount { get; set; }

    public string Currency { get; set; } = "TRY";

    // Pending, Accepted, Rejected, Cancelled
    public string Status { get; set; } = "Pending";

    public DateTime OfferDate { get; set; } = DateTime.UtcNow;

    public DateTime? ExpirationDate { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
