namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerJourneyAnalyticsDto
{
    public int UserId { get; set; }

    public string JourneyStage { get; set; } = string.Empty;

    public int TotalInteractions { get; set; }

    public int CompletedActions { get; set; }

    public decimal ConversionScore { get; set; }

    public string AnalyticsSummary { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
