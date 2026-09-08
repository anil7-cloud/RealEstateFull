namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerSalesLeadForecastBehaviorHistoryDto
{
    public int UserId { get; set; }

    public decimal BehaviorScore { get; set; }

    public string BehaviorLevel { get; set; } = string.Empty;

    public string BehaviorAnalysis { get; set; } = string.Empty;

    public List<string> Recommendations { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
