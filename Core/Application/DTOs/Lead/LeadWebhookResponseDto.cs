namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadWebhookResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string EventName { get; set; } = string.Empty;

    public string EndpointUrl { get; set; } = string.Empty;

    public string Payload { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
