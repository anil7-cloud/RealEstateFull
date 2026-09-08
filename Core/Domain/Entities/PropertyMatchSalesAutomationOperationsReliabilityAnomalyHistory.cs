namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsReliabilityAnomalyHistory
    {
        public Guid Id { get; set; }

        public string Severity { get; set; }
            = string.Empty;

        public decimal PreviousScore { get; set; }

        public decimal CurrentScore { get; set; }

        public decimal ScoreChange { get; set; }

        public decimal ScoreDrop { get; set; }

        public string Reason { get; set; }
            = string.Empty;

        /*
         * Reasons listesini JSON olarak saklayacağız.
         */
        public string ReasonsJson { get; set; }
            = "[]";

        public DateTime? PreviousSnapshotAt { get; set; }

        public DateTime? CurrentSnapshotAt { get; set; }

        public string Status { get; set; }
            = "Active";

        public DateTime DetectedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }
    }
}
