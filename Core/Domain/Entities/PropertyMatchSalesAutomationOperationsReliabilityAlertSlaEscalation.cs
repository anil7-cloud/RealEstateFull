namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsReliabilityAlertSlaEscalation
    {
        public Guid Id { get; set; }

        public Guid BreachId { get; set; }

        public Guid AlertId { get; set; }

        public string AlertNumber { get; set; }
            = string.Empty;

        public string BreachType { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string EscalationLevel { get; set; }
            = "L1";

        public decimal OverdueMinutes { get; set; }

        public string Status { get; set; }
            = "Active";

        public DateTime EscalatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }
    }
}
