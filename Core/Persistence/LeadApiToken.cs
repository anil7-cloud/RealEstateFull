namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadApiToken
{
    public int Id { get; set; }

    public int? LeadApiKeyId { get; set; }

    public int? UserId { get; set; }

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public string TokenType { get; set; } = "Bearer";

    public string Scope { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsRevoked { get; set; }

    public string DeviceName { get; set; } = string.Empty;

    public string DeviceId { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

    public DateTime ExpiresAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string RevocationReason { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
