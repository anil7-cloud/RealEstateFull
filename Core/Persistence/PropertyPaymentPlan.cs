namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyPaymentPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyContractId { get; set; }

    public decimal TotalAmount { get; set; }

    public int InstallmentCount { get; set; }

    public decimal InstallmentAmount { get; set; }

    public string PaymentStatus { get; set; } = "Pending";

    public DateTime StartDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PropertyContract? PropertyContract { get; set; }
}
