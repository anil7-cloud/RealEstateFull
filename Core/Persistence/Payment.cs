namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class Payment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "TRY";

    public string PaymentMethod { get; set; } = string.Empty;

    public string TransactionId { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime PaidAt { get; set; } = DateTime.UtcNow;
}
