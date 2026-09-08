namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadActivityReport
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public DateTime ReportDate { get; set; } = DateTime.UtcNow;

    public int TotalCalls { get; set; }

    public int TotalMeetings { get; set; }

    public int TotalEmails { get; set; }

    public int TotalSms { get; set; }

    public int TotalVisits { get; set; }

    public int TotalTasksCompleted { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal RevenueGenerated { get; set; }

    public int PerformanceScore { get; set; }

    public string Summary { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
