using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationPersistentAuditService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationAuditRepository _repository;

        public PropertyCustomerMatchSalesAutomationPersistentAuditService(
            PropertyCustomerMatchSalesAutomationAuditRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropertyMatchSalesAutomationAudit>
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
                string? metadata = null,
                bool isSuccessful = true,
                string? errorCode = null,
                string? errorMessage = null,
                string? correlationId = null,
                string? source = null,
                string? ipAddress = null,
                long? durationMilliseconds = null)
        {
            var now = DateTime.UtcNow;

            var entity =
                new PropertyMatchSalesAutomationAudit
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
                        message?.Trim()
                        ?? string.Empty,

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
                        metadata?.Trim()
                        ?? string.Empty,

                    IsSuccessful =
                        isSuccessful,

                    ErrorCode =
                        errorCode?.Trim(),

                    ErrorMessage =
                        errorMessage?.Trim(),

                    CorrelationId =
                        correlationId?.Trim(),

                    Source =
                        source?.Trim(),

                    IpAddress =
                        ipAddress?.Trim(),

                    DurationMilliseconds =
                        durationMilliseconds,

                    CreatedAt =
                        now,

                    CompletedAt =
                        durationMilliseconds.HasValue
                            ? now
                            : null
                };

            return await _repository.AddAsync(entity);
        }

        public Task<List<PropertyMatchSalesAutomationAudit>>
            GetRecentAsync(int limit = 100)
        {
            return _repository.GetRecentAsync(limit);
        }

        public Task<PropertyMatchSalesAutomationAudit?>
            GetByIdAsync(Guid auditId)
        {
            return _repository.GetByAuditIdAsync(auditId);
        }

        public Task<List<PropertyMatchSalesAutomationAudit>>
            GetForMatchAsync(
                int matchId,
                int limit = 100)
        {
            return _repository.GetForMatchAsync(
                matchId,
                limit);
        }

        public Task<List<PropertyMatchSalesAutomationAudit>>
            GetForLeadAsync(
                int leadId,
                int limit = 100)
        {
            return _repository.GetForLeadAsync(
                leadId,
                limit);
        }

        public Task<List<PropertyMatchSalesAutomationAudit>>
            GetForExecutionAsync(
                Guid executionId,
                int limit = 100)
        {
            return _repository.GetForExecutionAsync(
                executionId,
                limit);
        }

        public Task<List<PropertyMatchSalesAutomationAudit>>
            GetFailuresAsync(int limit = 100)
        {
            return _repository.GetFailuresAsync(limit);
        }

        public async Task<
            PropertyMatchSalesAutomationPersistentAuditSummaryDto>
            GetSummaryAsync()
        {
            var now =
                DateTime.UtcNow;

            var totalTask =
                _repository.CountAsync();

            var failuresTask =
                _repository.CountFailuresAsync();

            var lastHourTask =
                _repository.CountSinceAsync(
                    now.AddHours(-1));

            var last24HoursTask =
                _repository.CountSinceAsync(
                    now.AddHours(-24));

            await Task.WhenAll(
                totalTask,
                failuresTask,
                lastHourTask,
                last24HoursTask);

            var total =
                await totalTask;

            var failures =
                await failuresTask;

            return new
                PropertyMatchSalesAutomationPersistentAuditSummaryDto
            {
                TotalEntries =
                    total,

                FailureEntries =
                    failures,

                SuccessfulEntries =
                    Math.Max(
                        0,
                        total - failures),

                LastHour =
                    await lastHourTask,

                Last24Hours =
                    await last24HoursTask,

                FailureRate =
                    Percentage(
                        failures,
                        total),

                SuccessRate =
                    Percentage(
                        Math.Max(
                            0,
                            total - failures),
                        total),

                GeneratedAt =
                    now
            };
        }

        public Task<bool>
            DeleteAsync(Guid auditId)
        {
            return _repository.DeleteAsync(auditId);
        }

        private static string Normalize(
            string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? "Unknown"
                : value.Trim();
        }

        private static decimal Percentage(
            int value,
            int total)
        {
            if (total <= 0)
                return 0;

            return Math.Round(
                ((decimal)value / total) * 100m,
                2);
        }
    }

    public class
        PropertyMatchSalesAutomationPersistentAuditSummaryDto
    {
        public int TotalEntries { get; set; }

        public int SuccessfulEntries { get; set; }

        public int FailureEntries { get; set; }

        public int LastHour { get; set; }

        public int Last24Hours { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
