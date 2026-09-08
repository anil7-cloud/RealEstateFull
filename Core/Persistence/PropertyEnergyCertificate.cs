namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyEnergyCertificate
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string EnergyClass { get; set; } = string.Empty;

    public decimal AnnualEnergyConsumption { get; set; }

    public decimal CarbonEmission { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string CertificateNumber { get; set; } = string.Empty;

    public bool IsValid { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
