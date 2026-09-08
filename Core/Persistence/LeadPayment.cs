namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPayment
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public int? InvoiceId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "TRY";

    // Cash, CreditCard, BankTransfer, Check...
    public string PaymentMethod { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    // Pending, Completed, Failed, Refunded
    public string Status { get; set; } = "Pending";

    public string TransactionNumber { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
