namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadNegotiationResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public decimal OfferAmount { get; set; }

    public decimal CounterOfferAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime NegotiationDate { get; set; }
}
