namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyPortfolioItemRequestDto
{
    public int PortfolioId { get; set; }

    public int PropertyId { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
