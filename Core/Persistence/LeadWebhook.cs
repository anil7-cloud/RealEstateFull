namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadWebhook
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string EventName { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string HttpMethod { get; set; } = "POST";

    public string HeadersJson { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string PayloadTemplateJson { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;

    public int RetryCount { get; set; }

    public int TimeoutSeconds { get; set; } = 30;

    public DateTime? LastTriggeredAt { get; set; }

    public bool LastSucceeded { get; set; }

    public string LastResponse { get; set; } = string.Empty;

    public int? LastStatusCode { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
