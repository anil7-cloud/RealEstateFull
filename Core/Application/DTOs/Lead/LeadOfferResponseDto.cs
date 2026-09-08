namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadOfferResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public decimal OfferAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime OfferDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
