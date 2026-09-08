
namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyUtilityBill

{

    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string BillType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime BillingPeriod { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public bool IsPaid { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

