namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationAuditService
    {
        private static readonly object SyncRoot = new();

        private static readonly List<
            PropertyMatchSalesAutomationAuditEntryDto> Entries = new();

        public Task<PropertyMatchSalesAutomationAuditEntryDto>
            WriteAsync(
                string eventType,
                string action,
                string status,
                string message,
                int? matchId = null,
                int? leadId = null,
                int? propertyId = null,
                Guid? executionId = null,
                string? actor = null,
                decimal? score = null,
                string? metadata = null)
        {
            var entry =
                new PropertyMatchSalesAutomationAuditEntryDto
                {
                    AuditId =
                        Guid.NewGuid(),

                    EventType =
                        Normalize(eventType),

                    Action =
                        Normalize(action),

                    Status =
                        Normalize(status),

                    Message =
                        message?.Trim() ??
                        string.Empty,

                    MatchId =
                        matchId,

                    LeadId =
                        leadId,

                    PropertyId =
                        propertyId,

                    ExecutionId =
                        executionId,

                    Actor =
                        string.IsNullOrWhiteSpace(actor)
                            ? "System"
                            : actor.Trim(),

                    Score =
                        score,

                    Metadata =
                        metadata?.Trim() ??
                        string.Empty,

                    CreatedAt =
                        DateTime.UtcNow
                };

            lock (SyncRoot)
            {
                Entries.Add(entry);

                TrimHistory();
            }

            return Task.FromResult(entry);
        }

        public Task<PropertyMatchSalesAutomationAuditEntryDto>
            LogDecisionAsync(
                PropertyMatchSalesAutomationDecisionDto decision,
                string actor = "DecisionEngine")
        {
            ArgumentNullException.ThrowIfNull(decision);

            return WriteAsync(
                eventType: "Decision",
                action: decision.RecommendedAction,
                status: decision.Decision,
                message: decision.Reason,
                matchId: decision.MatchId,
                leadId: decision.LeadId,
                propertyId: decision.PropertyId,
                actor: actor,
                score: decision.DecisionScore,
                metadata:
                    $"Confidence={decision.ConfidenceScore:N2};" +
                    $"Priority={decision.Priority};" +
                    $"Channel={decision.RecommendedChannel}");
        }

        public Task<PropertyMatchSalesAutomationAuditEntryDto>
            LogOrchestrationAsync(
                PropertyMatchSalesOrchestrationItemDto item,
                string actor = "Orchestrator")
        {
            ArgumentNullException.ThrowIfNull(item);

            return WriteAsync(
                eventType: "Orchestration",
                action: item.RecommendedAction,
                status: item.Status,
                message:
                    string.IsNullOrWhiteSpace(item.Error)
                        ? item.Message
                        : item.Error,
                matchId: item.MatchId,
                leadId: item.LeadId,
                propertyId: item.PropertyId,
                executionId: item.ExecutionId,
                actor: actor,
                score: item.DecisionScore,
                metadata:
                    $"Decision={item.Decision};" +
                    $"Confidence={item.ConfidenceScore:N2};" +
                    $"ExecutionCreated={item.ExecutionCreated};" +
                    $"ExecutionStarted={item.ExecutionStarted}");
        }

        public Task<PropertyMatchSalesAutomationAuditEntryDto>
            LogExecutionAsync(
                PropertyMatchSalesAutomationExecutionDto execution,
                string eventType = "Execution",
                string actor = "ExecutionEngine")
        {
            ArgumentNullException.ThrowIfNull(execution);

            return WriteAsync(
                eventType: eventType,
                action: execution.Action,
                status: execution.Status,
                message:
                    string.IsNullOrWhiteSpace(
                        execution.LastError)
                        ? execution.ActionTitle
                        : execution.LastError,
                matchId: execution.MatchId,
                leadId: execution.LeadId,
                propertyId: execution.PropertyId,
                executionId: execution.ExecutionId,
                actor: actor,
                score: execution.ExecutionScore,
                metadata:
                    $"Channel={execution.Channel};" +
                    $"RequiresApproval={execution.RequiresApproval}");
        }

        public Task<PropertyMatchSalesAutomationAuditEntryDto>
            LogApprovalAsync(
                PropertyMatchSalesAutomationExecutionDto execution,
                string approvedBy)
        {
            ArgumentNullException.ThrowIfNull(execution);

            return WriteAsync(
                eventType: "Approval",
                action: execution.Action,
                status: "Approved",
                message:
                    $"Execution {execution.ExecutionId} onaylandı.",
                matchId: execution.MatchId,
                leadId: execution.LeadId,
                propertyId: execution.PropertyId,
                executionId: execution.ExecutionId,
                actor:
                    string.IsNullOrWhiteSpace(approvedBy)
                        ? "Unknown"
                        : approvedBy,
                score: execution.ExecutionScore);
        }

        public Task<PropertyMatchSalesAutomationAuditEntryDto>
            LogFailureAsync(
                PropertyMatchSalesAutomationExecutionDto execution,
                string? error = null,
                string actor = "ExecutionEngine")
        {
            ArgumentNullException.ThrowIfNull(execution);

            var message =
                !string.IsNullOrWhiteSpace(error)
                    ? error
                    : execution.LastError;

            return WriteAsync(
                eventType: "Failure",
                action: execution.Action,
                status: "Failed",
                message:
                    string.IsNullOrWhiteSpace(message)
                        ? "Execution başarısız oldu."
                        : message,
                matchId: execution.MatchId,
                leadId: execution.LeadId,
                propertyId: execution.PropertyId,
                executionId: execution.ExecutionId,
                actor: actor,
                score: execution.ExecutionScore);
        }

        public Task<List<PropertyMatchSalesAutomationAuditEntryDto>>
            GetRecentAsync(int limit = 100)
        {
            limit =
                NormalizeLimit(limit);

            lock (SyncRoot)
            {
                return Task.FromResult(
                    Entries
                        .OrderByDescending(
                            x => x.CreatedAt)
                        .Take(limit)
                        .Select(Clone)
                        .ToList());
            }
        }

        public Task<PropertyMatchSalesAutomationAuditEntryDto?>
            GetByIdAsync(Guid auditId)
        {
            lock (SyncRoot)
            {
                var result =
                    Entries.FirstOrDefault(
                        x => x.AuditId == auditId);

                return Task.FromResult(
                    result == null
                        ? null
                        : Clone(result));
            }
        }

        public Task<List<PropertyMatchSalesAutomationAuditEntryDto>>
            GetForMatchAsync(
                int matchId,
                int limit = 100)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            limit =
                NormalizeLimit(limit);

            lock (SyncRoot)
            {
                return Task.FromResult(
                    Entries
                        .Where(x =>
                            x.MatchId == matchId)
                        .OrderByDescending(
                            x => x.CreatedAt)
                        .Take(limit)
                        .Select(Clone)
                        .ToList());
            }
        }

        public Task<List<PropertyMatchSalesAutomationAuditEntryDto>>
            GetForLeadAsync(
                int leadId,
                int limit = 100)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            limit =
                NormalizeLimit(limit);

            lock (SyncRoot)
            {
                return Task.FromResult(
                    Entries
                        .Where(x =>
                            x.LeadId == leadId)
                        .OrderByDescending(
                            x => x.CreatedAt)
                        .Take(limit)
                        .Select(Clone)
                        .ToList());
            }
        }

        public Task<List<PropertyMatchSalesAutomationAuditEntryDto>>
            GetForExecutionAsync(
                Guid executionId,
                int limit = 100)
        {
            limit =
                NormalizeLimit(limit);

            lock (SyncRoot)
            {
                return Task.FromResult(
                    Entries
                        .Where(x =>
                            x.ExecutionId ==
                            executionId)
                        .OrderByDescending(
                            x => x.CreatedAt)
                        .Take(limit)
                        .Select(Clone)
                        .ToList());
            }
        }

        public Task<List<PropertyMatchSalesAutomationAuditEntryDto>>
            GetFailuresAsync(int limit = 50)
        {
            limit =
                NormalizeLimit(limit);

            lock (SyncRoot)
            {
                return Task.FromResult(
                    Entries
                        .Where(x =>
                            string.Equals(
                                x.Status,
                                "Failed",
                                StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(
                                x.EventType,
                                "Failure",
                                StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(
                            x => x.CreatedAt)
                        .Take(limit)
                        .Select(Clone)
                        .ToList());
            }
        }

        public Task<PropertyMatchSalesAutomationAuditSummaryDto>
            GetSummaryAsync()
        {
            lock (SyncRoot)
            {
                var now =
                    DateTime.UtcNow;

                var last24Hours =
                    now.AddHours(-24);

                var lastHour =
                    now.AddHours(-1);

                var snapshot =
                    Entries.ToList();

                return Task.FromResult(
                    new PropertyMatchSalesAutomationAuditSummaryDto
                    {
                        TotalEntries =
                            snapshot.Count,

                        Last24Hours =
                            snapshot.Count(x =>
                                x.CreatedAt >=
                                last24Hours),

                        LastHour =
                            snapshot.Count(x =>
                                x.CreatedAt >=
                                lastHour),

                        DecisionEvents =
                            CountEvent(
                                snapshot,
                                "Decision"),

                        ExecutionEvents =
                            CountEvent(
                                snapshot,
                                "Execution"),

                        OrchestrationEvents =
                            CountEvent(
                                snapshot,
                                "Orchestration"),

                        ApprovalEvents =
                            CountEvent(
                                snapshot,
                                "Approval"),

                        FailureEvents =
                            snapshot.Count(x =>
                                x.EventType ==
                                    "Failure" ||
                                x.Status ==
                                    "Failed"),

                        SystemEvents =
                            CountEvent(
                                snapshot,
                                "System"),

                        UniqueMatches =
                            snapshot
                                .Where(x =>
                                    x.MatchId.HasValue)
                                .Select(x =>
                                    x.MatchId!.Value)
                                .Distinct()
                                .Count(),

                        UniqueLeads =
                            snapshot
                                .Where(x =>
                                    x.LeadId.HasValue)
                                .Select(x =>
                                    x.LeadId!.Value)
                                .Distinct()
                                .Count(),

                        UniqueExecutions =
                            snapshot
                                .Where(x =>
                                    x.ExecutionId.HasValue)
                                .Select(x =>
                                    x.ExecutionId!.Value)
                                .Distinct()
                                .Count(),

                        GeneratedAt =
                            now
                    });
            }
        }

        public Task ClearAsync()
        {
            lock (SyncRoot)
            {
                Entries.Clear();
            }

            return Task.CompletedTask;
        }

        private static int CountEvent(
            IEnumerable<
                PropertyMatchSalesAutomationAuditEntryDto> entries,
            string eventType)
        {
            return entries.Count(x =>
                string.Equals(
                    x.EventType,
                    eventType,
                    StringComparison.OrdinalIgnoreCase));
        }

        private static void TrimHistory()
        {
            const int maximumEntries =
                10000;

            if (Entries.Count <= maximumEntries)
                return;

            var removeCount =
                Entries.Count -
                maximumEntries;

            Entries.RemoveRange(
                0,
                removeCount);
        }

        private static
            PropertyMatchSalesAutomationAuditEntryDto
            Clone(
                PropertyMatchSalesAutomationAuditEntryDto source)
        {
            return new
                PropertyMatchSalesAutomationAuditEntryDto
            {
                AuditId =
                    source.AuditId,

                EventType =
                    source.EventType,

                Action =
                    source.Action,

                Status =
                    source.Status,

                Message =
                    source.Message,

                MatchId =
                    source.MatchId,

                LeadId =
                    source.LeadId,

                PropertyId =
                    source.PropertyId,

                ExecutionId =
                    source.ExecutionId,

                Actor =
                    source.Actor,

                Score =
                    source.Score,

                Metadata =
                    source.Metadata,

                CreatedAt =
                    source.CreatedAt
            };
        }

        private static string Normalize(
            string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? "Unknown"
                : value.Trim();
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(
                limit,
                500);
        }
    }

    public class PropertyMatchSalesAutomationAuditEntryDto
    {
        public Guid AuditId { get; set; }

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
    }

    public class PropertyMatchSalesAutomationAuditSummaryDto
    {
        public int TotalEntries { get; set; }

        public int Last24Hours { get; set; }

        public int LastHour { get; set; }

        public int DecisionEvents { get; set; }

        public int ExecutionEvents { get; set; }

        public int OrchestrationEvents { get; set; }

        public int ApprovalEvents { get; set; }

        public int FailureEvents { get; set; }

        public int SystemEvents { get; set; }

        public int UniqueMatches { get; set; }

        public int UniqueLeads { get; set; }

        public int UniqueExecutions { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
