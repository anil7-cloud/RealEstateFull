namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadAnalyticsResponseDto
{
    public int TotalLeads { get; set; }

    public int NewLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public int LostLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal AverageDealValue { get; set; }

    public decimal TotalRevenue { get; set; }

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
