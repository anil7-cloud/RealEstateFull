namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadCommunicationResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string CommunicationType { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Direction { get; set; } = string.Empty;

    public DateTime CommunicationDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
