namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushWebhookDelivery
{
    public int Id { get; set; }

    public int LeadPushWebhookId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string Payload { get; set; } = string.Empty;

    public string ResponseBody { get; set; } = string.Empty;

    public int ResponseStatusCode { get; set; }

    public bool IsSuccessful { get; set; }

    public int RetryCount { get; set; }

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public LeadPushWebhook? Webhook { get; set; }
}
