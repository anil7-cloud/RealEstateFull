namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushABTest
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int TemplateAId { get; set; }

    public int TemplateBId { get; set; }

    public int TrafficPercentageA { get; set; } = 50;

    public int TrafficPercentageB { get; set; } = 50;

    public string SuccessMetric { get; set; } = "OpenRate";

    public bool IsActive { get; set; } = true;

    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public LeadPushTemplate? TemplateA { get; set; }

    public LeadPushTemplate? TemplateB { get; set; }
}
