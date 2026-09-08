namespace REAL_ESTATE_CLEAN.Core.Domain.Entities
{
    public class PropertyMatchSalesAutomationAudit
    {
        public long Id { get; set; }

        public Guid AuditId { get; set; }
            = Guid.NewGuid();

        public string EventType { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public string Message { get; set; }
            = string.Empty;

        public int? MatchId { get; set; }

        public int? LeadId { get; set; }

        public int? PropertyId { get; set; }

        public Guid? ExecutionId { get; set; }

        public string Actor { get; set; }
            = string.Empty;

        public decimal? Score { get; set; }

        public string Metadata { get; set; }
            = string.Empty;

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public string? IpAddress { get; set; }

        public string? CorrelationId { get; set; }

        public string? Source { get; set; }

        public bool IsSuccessful { get; set; }

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public long? DurationMilliseconds { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
