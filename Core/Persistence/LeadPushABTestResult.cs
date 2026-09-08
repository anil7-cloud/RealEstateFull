namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushABTestResult
{
    public int Id { get; set; }

    public int LeadPushABTestId { get; set; }

    public string Variant { get; set; } = string.Empty; // A veya B

    public int SentCount { get; set; }

    public int DeliveredCount { get; set; }

    public int OpenedCount { get; set; }

    public int ClickedCount { get; set; }

    public int ConversionCount { get; set; }

    public decimal OpenRate { get; set; }

    public decimal ClickRate { get; set; }

    public decimal ConversionRate { get; set; }

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public LeadPushABTest? ABTest { get; set; }
}
