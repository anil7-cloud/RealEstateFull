namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class PropertyWorkflowMetric
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PropertyWorkflowId { get; set; }

    public string MetricName { get; set; } = string.Empty;

    public decimal MetricValue { get; set; }

    public string? Unit { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public PropertyWorkflow? PropertyWorkflow { get; set; }
}
