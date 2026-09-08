namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationRetryAlertHistory
    {
        public Guid Id { get; set; }

        public Guid AlertId { get; set; }

        public string AlertCode { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string? PreviousStatus { get; set; }

        public string NewStatus { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string? PerformedBy { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
