namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationOperationsHealthIncident
    {
        public Guid Id { get; set; }

        public string IncidentNumber { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Reason { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = "Open";

        public decimal FirstScore { get; set; }

        public decimal LatestScore { get; set; }

        public decimal PeakScore { get; set; }

        public decimal ScoreDrop { get; set; }

        public int SnapshotCount { get; set; }

        public DateTime WindowStart { get; set; }

        public DateTime WindowEnd { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }
    }
}
