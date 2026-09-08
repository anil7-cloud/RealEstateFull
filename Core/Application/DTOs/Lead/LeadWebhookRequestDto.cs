namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadWebhookRequestDto
{
    public int LeadId { get; set; }

    public string EventName { get; set; } = string.Empty;

    public string EndpointUrl { get; set; } = string.Empty;

    public string Payload { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
