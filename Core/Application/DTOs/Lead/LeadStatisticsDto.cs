namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadStatisticsDto
{
    public int TotalLeads { get; set; }

    public int NewLeads { get; set; }

    public int ContactedLeads { get; set; }

    public int QualifiedLeads { get; set; }

    public int WonLeads { get; set; }

    public int LostLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal AverageResponseTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

