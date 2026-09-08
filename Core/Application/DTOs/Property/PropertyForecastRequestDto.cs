namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyForecastRequestDto
{
    public int? PropertyId { get; set; }

    public decimal ExpectedValue { get; set; }

    public decimal Probability { get; set; }

    public string ForecastStatus { get; set; } = string.Empty;

    public DateTime ForecastDate { get; set; } = DateTime.UtcNow;

    public int? CreatedByUserId { get; set; }
}
