namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadInvoice
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int PropertyId { get; set; }

    public int? UserId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public string Currency { get; set; } = "TRY";

    public DateTime InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    // Draft, Sent, Paid, Overdue, Cancelled
    public string Status { get; set; } = "Draft";

    public string FilePath { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
