namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadAnalytics
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public decimal MetricValue { get; set; }

    public string Category { get; set; } = string.Empty;

    public string Period { get; set; } = string.Empty;

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public decimal TargetValue { get; set; }

    public decimal Difference { get; set; }

    public string Trend { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
