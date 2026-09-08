namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyLeasePayment
{
    public int Id { get; set; }

    public int LeaseId { get; set; }

    public int PropertyId { get; set; }

    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
