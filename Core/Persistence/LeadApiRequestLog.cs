namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadApiRequestLog
{
    public int Id { get; set; }

    public int? LeadApiKeyId { get; set; }

    public string RequestId { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public string HttpMethod { get; set; } = string.Empty;

    public string RequestHeadersJson { get; set; } = string.Empty;

    public string RequestBodyJson { get; set; } = string.Empty;

    public string ResponseBodyJson { get; set; } = string.Empty;

    public int StatusCode { get; set; }

    public long DurationMilliseconds { get; set; }

    public string ClientIp { get; set; } = string.Empty;

    public string UserAgent { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RespondedAt { get; set; }
}
