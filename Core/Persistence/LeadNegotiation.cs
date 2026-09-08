namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadNegotiation
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public decimal InitialPrice { get; set; }

    public decimal CurrentOffer { get; set; }

    public decimal TargetPrice { get; set; }

    // Open, Accepted, Rejected, Cancelled
    public string Status { get; set; } = "Open";

    public int Round { get; set; }

    public DateTime LastNegotiationDate { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
