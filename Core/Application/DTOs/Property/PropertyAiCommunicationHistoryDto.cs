namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

public class PropertyAiCommunicationHistoryDto
{
    public int UserId { get; set; }

    public int PropertyId { get; set; }

    public string CommunicationType { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Direction { get; set; } = string.Empty;

    public DateTime CommunicationDate { get; set; } = DateTime.UtcNow;
}
