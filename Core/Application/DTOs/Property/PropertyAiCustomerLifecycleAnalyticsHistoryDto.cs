namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerLifecycleAnalyticsHistoryDto
{
    public int UserId { get; set; }

    public string LifecycleStage { get; set; } = string.Empty;

    public int TotalInteractions { get; set; }

    public int ActiveDays { get; set; }

    public decimal CustomerValue { get; set; }

    public decimal RetentionScore { get; set; }

    public string AnalyticsSummary { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
