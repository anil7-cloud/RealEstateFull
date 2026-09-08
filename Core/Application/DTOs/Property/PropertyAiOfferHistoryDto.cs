namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiOfferHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public decimal OfferAmount { get; set; }

    public string OfferStatus { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
