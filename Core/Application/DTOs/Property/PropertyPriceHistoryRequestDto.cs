namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyPriceHistoryRequestDto
{
    public int PropertyId { get; set; }

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public string ChangeReason { get; set; } = string.Empty;

    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;
}
