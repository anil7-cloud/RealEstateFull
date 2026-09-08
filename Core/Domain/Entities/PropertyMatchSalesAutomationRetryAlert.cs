namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationRetryAlert
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Category { get; set; }
            = string.Empty;

        public string Message { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = "Active";

        public string? AcknowledgedBy { get; set; }

        public string? ResolvedBy { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? AcknowledgedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}
