namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiTrendLogDto
{
    public int PropertyId { get; set; }

    public string TrendType { get; set; } = string.Empty;

    public decimal CurrentValue { get; set; }

    public decimal PreviousValue { get; set; }

    public decimal ChangeRate { get; set; }

    public string TrendAnalysis { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
