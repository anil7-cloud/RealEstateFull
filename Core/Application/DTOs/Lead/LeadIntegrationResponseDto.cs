namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadIntegrationResponseDto
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public string IntegrationName { get; set; } = string.Empty;

    public string ExternalId { get; set; } = string.Empty;

    public string SyncStatus { get; set; } = string.Empty;

    public DateTime LastSyncDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
