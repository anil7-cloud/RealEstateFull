namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCustomerLifecycleHistoryDto
{
    public int UserId { get; set; }

    public string LifecycleStage { get; set; } = string.Empty;

    public string StageDescription { get; set; } = string.Empty;

    public decimal CustomerValue { get; set; }

    public decimal RetentionScore { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
