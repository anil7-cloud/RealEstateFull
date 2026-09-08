namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Forecast;

public class ForecastResultDto
{
    public int UserId { get; set; }

    public double PredictionScore { get; set; }

    public double ConversionProbability { get; set; }

    public decimal ExpectedRevenue { get; set; }
}
