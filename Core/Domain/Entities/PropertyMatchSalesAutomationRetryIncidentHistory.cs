namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationRetryIncidentHistory
    {
        public Guid Id { get; set; }

        public Guid IncidentId { get; set; }

        public string IncidentNumber { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string? PreviousStatus { get; set; }

        public string NewStatus { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public decimal RiskScore { get; set; }

        public string? PerformedBy { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
