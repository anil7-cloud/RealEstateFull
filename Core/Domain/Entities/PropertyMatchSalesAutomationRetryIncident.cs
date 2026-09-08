namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationRetryIncident
    {
        public Guid Id { get; set; }

        public string IncidentNumber { get; set; }
            = string.Empty;

        public string Title { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = "Open";

        public decimal RiskScore { get; set; }

        public string RiskLevel { get; set; }
            = string.Empty;

        public string? Description { get; set; }

        public string? AssignedTo { get; set; }

        public string? ResolvedBy { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? AcknowledgedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}
