namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushQueue
{
    public int Id { get; set; }

    public int LeadPushTaskId { get; set; }

    public string QueueName { get; set; } = "default";

    public int Priority { get; set; }

    public string Status { get; set; } = "Waiting";

    public int AttemptCount { get; set; }

    public DateTime AvailableAt { get; set; } = DateTime.UtcNow;

    public DateTime? ProcessedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string WorkerName { get; set; } = string.Empty;

    public LeadPushTask? Task { get; set; }
}
