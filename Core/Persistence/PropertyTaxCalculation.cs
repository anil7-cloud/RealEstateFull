namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyTaxCalculation
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public decimal PropertyValue { get; set; }

    public decimal TaxRate { get; set; }

    public decimal AnnualTaxAmount { get; set; }

    public int TaxYear { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
