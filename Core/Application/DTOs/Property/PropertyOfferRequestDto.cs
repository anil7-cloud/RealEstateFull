namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyOfferRequestDto
{
    public int PropertyId { get; set; }

    public int UserId { get; set; }

    public decimal OfferAmount { get; set; }

    public string OfferStatus { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime OfferDate { get; set; } = DateTime.UtcNow;
}
