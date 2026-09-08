namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadOfferDto
{
    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public decimal OfferAmount { get; set; }

    public string Currency { get; set; } = "TRY";

    public string Status { get; set; } = "Pending";

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
