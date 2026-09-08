namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadKpiResponseDto
{
    public string KpiName { get; set; } = string.Empty;

    public decimal CurrentValue { get; set; }

    public decimal TargetValue { get; set; }

    public decimal AchievementRate { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}
