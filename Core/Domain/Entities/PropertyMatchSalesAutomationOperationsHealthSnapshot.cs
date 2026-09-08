namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsHealthSnapshot
    {
        public Guid Id { get; set; }

        public decimal Score { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public decimal ReliabilityScore { get; set; }

        public decimal AnomalyPenalty { get; set; }

        public decimal EscalationPenalty { get; set; }

        public bool HasAnomaly { get; set; }

        public string AnomalySeverity { get; set; }
            = string.Empty;

        public int ActiveEscalations { get; set; }

        public string HighestEscalationLevel { get; set; }
            = "None";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
