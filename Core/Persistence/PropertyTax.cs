namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyTax
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int TaxYear { get; set; }

    public decimal TaxAmount { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public bool IsPaid { get; set; }

    public string ReceiptNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
