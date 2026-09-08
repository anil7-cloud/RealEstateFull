namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiDemandForecastRequestDto
{
    public int PropertyId { get; set; }

    public decimal CurrentDemandScore { get; set; }

    public decimal ExpectedDemandScore { get; set; }

    public string ForecastPeriod { get; set; } = string.Empty;

    public string ForecastReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
