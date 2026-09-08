
namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class EnergyCertificate

{

    public int Id { get; set; }

    public int PropertyId { get; set; }

    public string CertificateNumber { get; set; } = string.Empty;

    public string EnergyClass { get; set; } = string.Empty;

    public DateTime IssueDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string DocumentUrl { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

