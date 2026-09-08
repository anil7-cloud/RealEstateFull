namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsReliabilityAlert
    {
        public Guid Id { get; set; }

        public Guid? AnomalyId { get; set; }

        public string AlertNumber { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string Title { get; set; }
            = string.Empty;

        public string Reason { get; set; }
            = string.Empty;

        public decimal Score { get; set; }

        public decimal ScoreDrop { get; set; }

        public string Status { get; set; }
            = "Open";

        public string? AcknowledgedBy { get; set; }

        public DateTime? AcknowledgedAt { get; set; }

        public string? ResolvedBy { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
