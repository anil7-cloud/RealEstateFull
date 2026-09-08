namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyNegotiationRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public decimal InitialOffer { get; set; }

    public decimal CounterOffer { get; set; }

    public string NegotiationStatus { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime NegotiationDate { get; set; } = DateTime.UtcNow;
}
