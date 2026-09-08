namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAnalyticsRequestDto
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? UserId { get; set; }

    public string? Source { get; set; }

    public string? Status { get; set; }

    public bool IncludeForecast { get; set; } = true;
}
