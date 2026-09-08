namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsReliabilityHistory
    {
        public Guid Id { get; set; }

        public decimal Score { get; set; }

        public string Grade { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public decimal TotalPenalty { get; set; }

        public decimal Bonus { get; set; }

        public decimal RiskScore { get; set; }

        public int OpenIncidents { get; set; }

        public int CriticalIncidents { get; set; }

        public int SlaBreaches { get; set; }

        public int Level2Escalations { get; set; }

        public int Level3Escalations { get; set; }

        public decimal MttrMinutes { get; set; }

        public decimal ResolutionRate { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
