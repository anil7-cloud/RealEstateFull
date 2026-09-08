namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyLease
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int TenantUserId { get; set; }

    public decimal MonthlyRent { get; set; }

    public decimal DepositAmount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int PaymentDay { get; set; }

    public bool IsActive { get; set; } = true;

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
