namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyFraudReport
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int ReporterUserId { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ReviewedAt { get; set; }
}
