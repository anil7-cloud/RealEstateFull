namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadPerformanceResponseDto
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public int TotalLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public int LostLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal TotalRevenue { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
