namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadCommunicationRequestDto
{
    public int LeadId { get; set; }

    public string CommunicationType { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Direction { get; set; } = "Outgoing";

    public int? UserId { get; set; }
}
