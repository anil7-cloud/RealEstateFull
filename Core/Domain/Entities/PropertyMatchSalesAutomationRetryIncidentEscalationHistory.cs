namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationRetryIncidentEscalationHistory
    {
        public Guid Id { get; set; }

        public Guid IncidentId { get; set; }

        public string IncidentNumber { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string? PreviousLevel { get; set; }

        public string NewLevel { get; set; }
            = string.Empty;

        public int PreviousLevelNumber { get; set; }

        public int NewLevelNumber { get; set; }

        public string Severity { get; set; }
            = string.Empty;

        public int OverdueMinutes { get; set; }

        public string? PerformedBy { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
