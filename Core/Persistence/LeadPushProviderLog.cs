namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadPushProviderLog
{
    public int Id { get; set; }

    public int LeadPushProviderId { get; set; }

    public int? LeadPushCampaignId { get; set; }

    public int? LeadPushNotificationQueueId { get; set; }

    public string RequestId { get; set; } = string.Empty;

    public string RequestPayload { get; set; } = string.Empty;

    public string ResponsePayload { get; set; } = string.Empty;

    public string ResponseCode { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }

    public int StatusCode { get; set; }

    public int DurationMs { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RespondedAt { get; set; }

    public string IpAddress { get; set; } = string.Empty;

    public string UserAgent { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
