namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsReliabilityAlertHistory
    {
        public Guid Id { get; set; }

        public Guid AlertId { get; set; }

        public string AlertNumber { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string? PreviousStatus { get; set; }

        public string NewStatus { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string? PerformedBy { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
