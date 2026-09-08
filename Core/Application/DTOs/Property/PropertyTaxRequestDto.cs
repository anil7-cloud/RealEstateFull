namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyTaxRequestDto
{
    public int PropertyId { get; set; }

    public string TaxType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime TaxDate { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
