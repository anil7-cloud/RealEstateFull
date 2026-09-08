namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadForecast
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public decimal EstimatedSalePrice { get; set; }

    public decimal EstimatedCommission { get; set; }

    public decimal Probability { get; set; }

    public decimal ExpectedRevenue { get; set; }

    public DateTime ExpectedClosingDate { get; set; }

    public string ForecastStatus { get; set; } = string.Empty;

    public string RiskLevel { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
