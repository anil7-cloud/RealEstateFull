namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadContract
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    // Reservation, Sale, Rental, Commission...
    public string ContractType { get; set; } = string.Empty;

    public string ContractNumber { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "TRY";

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    // Draft, Active, Completed, Cancelled
    public string Status { get; set; } = "Draft";

    public string FilePath { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
