namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationRetryEvent
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public Guid? NewJobId { get; set; }

        public Guid? ClaimToken { get; set; }

        public int RetryCount { get; set; }

        public string EventType { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public bool Successful { get; set; }

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime? NextRetryAt { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
