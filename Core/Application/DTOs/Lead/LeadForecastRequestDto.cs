namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadForecastRequestDto
{
    public int LeadId { get; set; }

    public decimal EstimatedValue { get; set; }

    public decimal Probability { get; set; }

    public string ForecastStatus { get; set; } = string.Empty;

    public int CreatedByUserId { get; set; }
}
