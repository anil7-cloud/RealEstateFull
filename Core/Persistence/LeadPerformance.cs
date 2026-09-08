namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPerformance
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? UserId { get; set; }

    public int CallsCount { get; set; }

    public int MeetingsCount { get; set; }

    public int VisitsCount { get; set; }

    public int EmailsCount { get; set; }

    public int SmsCount { get; set; }

    public int TasksCompleted { get; set; }

    public decimal ConversionRate { get; set; }

    public decimal EstimatedRevenue { get; set; }

    public decimal ActualRevenue { get; set; }

    public int PerformanceScore { get; set; }

    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
