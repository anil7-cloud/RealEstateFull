namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationRetryIncidentEscalation
    {
        public Guid Id { get; set; }

        public Guid IncidentId { get; set; }

        public string IncidentNumber { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Level { get; set; }
            = "Level1";

        public int LevelNumber { get; set; }

        public int OverdueMinutes { get; set; }

        public string Status { get; set; }
            = "Active";

        public string? RecommendedAction { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }
    }
}
