namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsReliabilityAlertSlaBreach
    {
        public Guid Id { get; set; }

        public Guid AlertId { get; set; }

        public string AlertNumber { get; set; }
            = string.Empty;

        public string BreachType { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public decimal LimitMinutes { get; set; }

        public decimal ActualMinutes { get; set; }

        public decimal OverdueMinutes { get; set; }

        public string Status { get; set; }
            = "Active";

        public DateTime DetectedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }
    }
}
