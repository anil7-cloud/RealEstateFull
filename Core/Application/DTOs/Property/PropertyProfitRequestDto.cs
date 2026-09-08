namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyProfitRequestDto
{
    public int PropertyId { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public decimal ExpenseAmount { get; set; }

    public decimal ProfitAmount { get; set; }

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
