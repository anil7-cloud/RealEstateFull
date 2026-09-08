namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyInsurance
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string InsuranceType { get; set; } = string.Empty;

    public string PolicyNumber { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    public decimal PremiumAmount { get; set; }

    public decimal CoverageAmount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
