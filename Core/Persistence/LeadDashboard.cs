namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadDashboard
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime DashboardDate { get; set; } = DateTime.UtcNow;

    public int TotalLeads { get; set; }

    public int NewLeads { get; set; }

    public int QualifiedLeads { get; set; }

    public int ConvertedLeads { get; set; }

    public int LostLeads { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal TotalRevenue { get; set; }

    public decimal TotalCommission { get; set; }

    public int OpenTasks { get; set; }

    public int UpcomingMeetings { get; set; }

    public int PendingCalls { get; set; }

    public int PerformanceScore { get; set; }

    public string Summary { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
