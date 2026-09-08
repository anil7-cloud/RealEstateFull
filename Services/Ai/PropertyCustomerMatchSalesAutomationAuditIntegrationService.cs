using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationAuditIntegrationService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationAuditService
                _memoryAuditService;

        private readonly
            PropertyCustomerMatchSalesAutomationPersistentAuditService
                _persistentAuditService;

        public PropertyCustomerMatchSalesAutomationAuditIntegrationService(
            PropertyCustomerMatchSalesAutomationAuditService memoryAuditService,
            PropertyCustomerMatchSalesAutomationPersistentAuditService persistentAuditService)
        {
            _memoryAuditService =
                memoryAuditService;

            _persistentAuditService =
                persistentAuditService;
        }

        public async Task<PropertyMatchSalesAutomationAuditIntegrationResultDto>
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
                string? errorMessage = null)
        {
            PropertyMatchSalesAutomationAuditEntryDto?
                memoryEntry = null;

            PropertyMatchSalesAutomationAudit?
                persistentEntry = null;

            Exception?
                memoryException = null;

            Exception?
                persistentException = null;

            try
            {
                memoryEntry =
                    await _memoryAuditService.WriteAsync(
                        eventType,
                        action,
                        status,
                        message,
                        matchId,
                        leadId,
                        propertyId,
                        executionId,
                        actor,
                        score,
                        metadata);
            }
            catch (Exception ex)
            {
                memoryException = ex;
            }

            try
            {
                persistentEntry =
                    await _persistentAuditService.WriteAsync(
                        eventType: eventType,
                        action: action,
                        status: status,
                        message: message,
                        matchId: matchId,
                        leadId: leadId,
                        propertyId: propertyId,
                        executionId: executionId,
                        actor: actor,
                        score: score,
                        metadata: metadata,
                        isSuccessful: isSuccessful,
                        errorCode: errorCode,
                        errorMessage: errorMessage);
            }
            catch (Exception ex)
            {
                persistentException = ex;
            }

            var memorySucceeded =
                memoryEntry != null;

            var persistentSucceeded =
                persistentEntry != null;

            return new
                PropertyMatchSalesAutomationAuditIntegrationResultDto
            {
                Successful =
                    memorySucceeded ||
                    persistentSucceeded,

                FullyPersisted =
                    memorySucceeded &&
                    persistentSucceeded,

                MemorySucceeded =
                    memorySucceeded,

                PersistentSucceeded =
                    persistentSucceeded,

                MemoryAuditId =
                    memoryEntry?.AuditId,

                PersistentAuditId =
                    persistentEntry?.AuditId,

                EventType =
                    eventType,

                Action =
                    action,

                Status =
                    status,

                MemoryError =
                    memoryException?.Message,

                PersistentError =
                    persistentException?.Message,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<PropertyMatchSalesAutomationAuditCombinedSummaryDto>
            GetSummaryAsync()
        {
            var memoryTask =
                _memoryAuditService.GetSummaryAsync();

            PropertyMatchSalesAutomationPersistentAuditSummaryDto?
                persistent = null;

            string? persistentError =
                null;

            try
            {
                persistent =
                    await _persistentAuditService
                        .GetSummaryAsync();
            }
            catch (Exception ex)
            {
                persistentError =
                    ex.Message;
            }

            var memory =
                await memoryTask;

            return new
                PropertyMatchSalesAutomationAuditCombinedSummaryDto
            {
                MemoryEntries =
                    memory.TotalEntries,

                PersistentEntries =
                    persistent?.TotalEntries ?? 0,

                MemoryFailures =
                    memory.FailureEvents,

                PersistentFailures =
                    persistent?.FailureEntries ?? 0,

                PersistentAvailable =
                    persistent != null,

                PersistentError =
                    persistentError,

                Last24HoursMemory =
                    memory.Last24Hours,

                Last24HoursPersistent =
                    persistent?.Last24Hours ?? 0,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<
            List<PropertyMatchSalesAutomationAuditEntryDto>>
            GetMemoryRecentAsync(
                int limit = 100)
        {
            return await _memoryAuditService
                .GetRecentAsync(
                    NormalizeLimit(limit));
        }

        public async Task<
            List<PropertyMatchSalesAutomationAudit>>
            GetPersistentRecentAsync(
                int limit = 100)
        {
            return await _persistentAuditService
                .GetRecentAsync(
                    NormalizeLimit(limit));
        }

        public async Task<PropertyMatchSalesAutomationAuditStorageHealthDto>
            GetStorageHealthAsync()
        {
            var summary =
                await GetSummaryAsync();

            var status =
                summary.PersistentAvailable
                    ? "Healthy"
                    : "Degraded";

            return new
                PropertyMatchSalesAutomationAuditStorageHealthDto
            {
                Status =
                    status,

                MemoryAvailable =
                    true,

                PersistentAvailable =
                    summary.PersistentAvailable,

                MemoryEntries =
                    summary.MemoryEntries,

                PersistentEntries =
                    summary.PersistentEntries,

                PersistentError =
                    summary.PersistentError,

                Healthy =
                    summary.PersistentAvailable,

                GeneratedAt =
                    DateTime.UtcNow
            };
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

    public class PropertyMatchSalesAutomationAuditIntegrationResultDto
    {
        public bool Successful { get; set; }

        public bool FullyPersisted { get; set; }

        public bool MemorySucceeded { get; set; }

        public bool PersistentSucceeded { get; set; }

        public Guid? MemoryAuditId { get; set; }

        public Guid? PersistentAuditId { get; set; }

        public string EventType { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public string? MemoryError { get; set; }

        public string? PersistentError { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationAuditCombinedSummaryDto
    {
        public int MemoryEntries { get; set; }

        public int PersistentEntries { get; set; }

        public int MemoryFailures { get; set; }

        public int PersistentFailures { get; set; }

        public int Last24HoursMemory { get; set; }

        public int Last24HoursPersistent { get; set; }

        public bool PersistentAvailable { get; set; }

        public string? PersistentError { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationAuditStorageHealthDto
    {
        public string Status { get; set; }
            = string.Empty;

        public bool Healthy { get; set; }

        public bool MemoryAvailable { get; set; }

        public bool PersistentAvailable { get; set; }

        public int MemoryEntries { get; set; }

        public int PersistentEntries { get; set; }

        public string? PersistentError { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
