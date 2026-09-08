namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadWebhookHistory
{
    public int Id { get; set; }

    public int LeadWebhookId { get; set; }

    public string EventName { get; set; } = string.Empty;

    public string RequestUrl { get; set; } = string.Empty;

    public string HttpMethod { get; set; } = "POST";

    public string RequestHeadersJson { get; set; } = string.Empty;

    public string RequestBodyJson { get; set; } = string.Empty;

    public int? ResponseStatusCode { get; set; }

    public string ResponseBody { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }

    public int RetryNumber { get; set; }

    public long DurationMilliseconds { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }
}
