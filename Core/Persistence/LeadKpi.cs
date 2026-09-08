namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadKpi
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public string KpiName { get; set; } = string.Empty;

    public decimal CurrentValue { get; set; }

    public decimal TargetValue { get; set; }

    public decimal MinimumValue { get; set; }

    public decimal MaximumValue { get; set; }

    public decimal AchievementPercentage { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
