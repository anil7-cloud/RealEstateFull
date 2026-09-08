namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadDashboardResponseDto
{
    public int TotalLeads { get; set; }

    public int TodayLeads { get; set; }

    public int PendingLeads { get; set; }

    public int ContactedLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public int LostLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal TotalDealValue { get; set; }

    public DateTime GeneratedAt { get; set; }
}
