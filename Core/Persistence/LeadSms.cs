namespace REAL_ESTATE_CLEAN.Core.Persistence;

public class LeadSms
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public int? PropertyId { get; set; }

    public int? UserId { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Status { get; set; } = "Draft";

    public string Provider { get; set; } = string.Empty;

    public string ProviderMessageId { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime? SentAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
