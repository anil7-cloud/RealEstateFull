namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiContactHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string ContactType { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime ContactDate { get; set; } = DateTime.UtcNow;

    public bool IsSuccessful { get; set; }
}
