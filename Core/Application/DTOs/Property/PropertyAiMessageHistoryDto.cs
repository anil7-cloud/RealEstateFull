namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiMessageHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string Message { get; set; } = string.Empty;

    public string SenderType { get; set; } = string.Empty;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public bool IsRead { get; set; }
}
