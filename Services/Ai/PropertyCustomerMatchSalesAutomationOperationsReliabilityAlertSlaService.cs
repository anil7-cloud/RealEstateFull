using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertRepository
                _alertRepository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertRepository alertRepository)
        {
            _alertRepository =
                alertRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationReliabilityAlertSlaSummaryDto>
            GetSummaryAsync(
                CancellationToken cancellationToken = default)
        {
            var alerts =
                await _alertRepository
                    .GetRecentAsync(
                        5000,
                        cancellationToken);

            var now =
                DateTime.UtcNow;

            var results =
                alerts
                    .Select(x =>
                        Evaluate(
                            x,
                            now))
                    .ToList();

            return new()
            {
                TotalAlerts =
                    results.Count,

                BreachedAlerts =
                    results.Count(x =>
                        x.HasAnyBreach),

                AcknowledgeBreaches =
                    results.Count(x =>
                        x.AcknowledgeSlaBreached),

                ResolutionBreaches =
                    results.Count(x =>
                        x.ResolutionSlaBreached),

                P1Breaches =
                    results.Count(x =>
                        x.HasAnyBreach &&
                        x.Priority == "P1"),

                P2Breaches =
                    results.Count(x =>
                        x.HasAnyBreach &&
                        x.Priority == "P2"),

                ActiveBreaches =
                    results.Count(x =>
                        x.HasAnyBreach &&
                        x.Status != "Resolved"),

                Alerts =
                    results
                        .Where(x =>
                            x.HasAnyBreach)
                        .OrderByDescending(x =>
                            x.Priority == "P1")
                        .ThenByDescending(x =>
                            x.TotalOverdueMinutes)
                        .ToList(),

                GeneratedAt =
                    now
            };
        }

        private static
            PropertyMatchSalesAutomationReliabilityAlertSlaDto
            Evaluate(
                PropertyMatchSalesAutomationOperationsReliabilityAlert alert,
                DateTime now)
        {
            var policy =
                GetPolicy(
                    alert.Priority);

            var acknowledgeEnd =
                alert.AcknowledgedAt
                ?? now;

            var resolutionEnd =
                alert.ResolvedAt
                ?? now;

            var acknowledgeMinutes =
                Math.Max(
                    0m,
                    (decimal)
                    (
                        acknowledgeEnd -
                        alert.CreatedAt
                    ).TotalMinutes);

            var resolutionMinutes =
                Math.Max(
                    0m,
                    (decimal)
                    (
                        resolutionEnd -
                        alert.CreatedAt
                    ).TotalMinutes);

            var acknowledgeBreached =
                acknowledgeMinutes >
                policy.AcknowledgeMinutes;

            var resolutionBreached =
                resolutionMinutes >
                policy.ResolutionMinutes;

            var acknowledgeOverdue =
                acknowledgeBreached
                    ? acknowledgeMinutes -
                        policy.AcknowledgeMinutes
                    : 0m;

            var resolutionOverdue =
                resolutionBreached
                    ? resolutionMinutes -
                        policy.ResolutionMinutes
                    : 0m;

            return new()
            {
                AlertId =
                    alert.Id,

                AlertNumber =
                    alert.AlertNumber,

                Priority =
                    alert.Priority,

                Severity =
                    alert.Severity,

                Status =
                    alert.Status,

                AcknowledgeTargetMinutes =
                    policy.AcknowledgeMinutes,

                ResolutionTargetMinutes =
                    policy.ResolutionMinutes,

                ActualAcknowledgeMinutes =
                    Math.Round(
                        acknowledgeMinutes,
                        2),

                ActualResolutionMinutes =
                    Math.Round(
                        resolutionMinutes,
                        2),

                AcknowledgeSlaBreached =
                    acknowledgeBreached,

                ResolutionSlaBreached =
                    resolutionBreached,

                AcknowledgeOverdueMinutes =
                    Math.Round(
                        acknowledgeOverdue,
                        2),

                ResolutionOverdueMinutes =
                    Math.Round(
                        resolutionOverdue,
                        2),

                HasAnyBreach =
                    acknowledgeBreached ||
                    resolutionBreached,

                TotalOverdueMinutes =
                    Math.Round(
                        acknowledgeOverdue +
                        resolutionOverdue,
                        2),

                CreatedAt =
                    alert.CreatedAt,

                AcknowledgedAt =
                    alert.AcknowledgedAt,

                ResolvedAt =
                    alert.ResolvedAt
            };
        }

        private static
            PropertyMatchSalesAutomationReliabilityAlertSlaPolicy
            GetPolicy(
                string priority)
        {
            /*
             * Başlangıç SLA politikası:
             *
             * P1:
             * acknowledge <= 5 dakika
             * resolve     <= 30 dakika
             *
             * P2:
             * acknowledge <= 15 dakika
             * resolve     <= 120 dakika
             */

            if (string.Equals(
                    priority,
                    "P1",
                    StringComparison.OrdinalIgnoreCase))
            {
                return new()
                {
                    AcknowledgeMinutes = 5m,
                    ResolutionMinutes = 30m
                };
            }

            return new()
            {
                AcknowledgeMinutes = 15m,
                ResolutionMinutes = 120m
            };
        }

        private class
            PropertyMatchSalesAutomationReliabilityAlertSlaPolicy
        {
            public decimal AcknowledgeMinutes { get; set; }

            public decimal ResolutionMinutes { get; set; }
        }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaSummaryDto
    {
        public int TotalAlerts { get; set; }

        public int BreachedAlerts { get; set; }

        public int AcknowledgeBreaches { get; set; }

        public int ResolutionBreaches { get; set; }

        public int P1Breaches { get; set; }

        public int P2Breaches { get; set; }

        public int ActiveBreaches { get; set; }

        public List<
            PropertyMatchSalesAutomationReliabilityAlertSlaDto>
            Alerts { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaDto
    {
        public Guid AlertId { get; set; }

        public string AlertNumber { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public decimal AcknowledgeTargetMinutes { get; set; }

        public decimal ResolutionTargetMinutes { get; set; }

        public decimal ActualAcknowledgeMinutes { get; set; }

        public decimal ActualResolutionMinutes { get; set; }

        public bool AcknowledgeSlaBreached { get; set; }

        public bool ResolutionSlaBreached { get; set; }

        public decimal AcknowledgeOverdueMinutes { get; set; }

        public decimal ResolutionOverdueMinutes { get; set; }

        public bool HasAnyBreach { get; set; }

        public decimal TotalOverdueMinutes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? AcknowledgedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}
