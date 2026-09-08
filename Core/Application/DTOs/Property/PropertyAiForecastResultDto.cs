namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiForecastResultDto
{
    public int PropertyId { get; set; }

    public decimal PredictedPrice { get; set; }

    public decimal GrowthRate { get; set; }

    public string ForecastPeriod { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
