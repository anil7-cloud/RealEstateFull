namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.Lead;

public class LeadIntegrationDto
{
    public int LeadId { get; set; }

    public string IntegrationName { get; set; } = string.Empty;

    public string ExternalId { get; set; } = string.Empty;

    public string SyncStatus { get; set; } = "Pending";

    public DateTime LastSyncDate { get; set; } = DateTime.UtcNow;
}
