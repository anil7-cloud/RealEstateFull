namespace REAL_ESTATE_CLEAN.Core.Application.DTOs.AI
{
    public class PropertyCustomerMatchSalesAutomationRuntimeDto
    {
        public Guid RuntimeId { get; set; }
            = Guid.NewGuid();

        public int? MatchId { get; set; }

        public int? LeadId { get; set; }

        public int? PropertyId { get; set; }

        public Guid? ExecutionId { get; set; }

        public string Status { get; set; }
            = "Pending";

        public string CurrentStage { get; set; }
            = "Created";

        public bool DecisionCompleted { get; set; }

        public bool ExecutionCreated { get; set; }

        public bool ExecutionStarted { get; set; }

        public bool ExecutionCompleted { get; set; }

        public bool AuditPersisted { get; set; }

        public bool Successful { get; set; }

        public decimal? MatchScore { get; set; }

        public string? SelectedAction { get; set; }

        public string? FailureStage { get; set; }

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime StartedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public long DurationMilliseconds { get; set; }

        public List<PropertyCustomerMatchSalesAutomationRuntimeStageDto>
            Stages { get; set; } = new();
    }

    public class PropertyCustomerMatchSalesAutomationRuntimeStageDto
    {
        public string Stage { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public string? Message { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public long DurationMilliseconds { get; set; }

        public bool Successful { get; set; }

        public string? Error { get; set; }
    }

    public class PropertyCustomerMatchSalesAutomationRuntimeRequestDto
    {
        public int MatchId { get; set; }

        public int? LeadId { get; set; }

        public int? PropertyId { get; set; }

        public string Actor { get; set; }
            = "System";

        public bool RequireApproval { get; set; }

        public bool ExecuteImmediately { get; set; }
            = true;

        public bool PersistAudit { get; set; }
            = true;

        public string CorrelationId { get; set; }
            = Guid.NewGuid().ToString("N");

        public Dictionary<string, string> Metadata { get; set; }
            = new();
    }

    public class PropertyCustomerMatchSalesAutomationRuntimeHealthDto
    {
        public bool Healthy { get; set; }

        public string Status { get; set; }
            = "Unknown";

        public bool DecisionServiceAvailable { get; set; }

        public bool ExecutionServiceAvailable { get; set; }

        public bool AuditServiceAvailable { get; set; }

        public bool DatabaseAvailable { get; set; }

        public int ActiveExecutions { get; set; }

        public int SuccessfulExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public DateTime CheckedAt { get; set; }
            = DateTime.UtcNow;
    }
}
