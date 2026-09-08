namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class CommissionTracking
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyTransactionId { get; set; }

    public Guid AgentUserId { get; set; }

    public decimal CommissionRate { get; set; }

    public decimal CommissionAmount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PaidAt { get; set; }

    public PropertyTransaction? PropertyTransaction { get; set; }
}
