namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeaseAgreement
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int LandlordId { get; set; }

    public int TenantId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal MonthlyRent { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
