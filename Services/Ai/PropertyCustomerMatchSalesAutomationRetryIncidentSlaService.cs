using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentSlaService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentRepository
                _incidentRepository;

        public PropertyCustomerMatchSalesAutomationRetryIncidentSlaService(
            PropertyCustomerMatchSalesAutomationRetryIncidentRepository incidentRepository)
        {
            _incidentRepository =
                incidentRepository;
        }

        public PropertyMatchSalesAutomationRetryIncidentSlaDto
            Evaluate(
                PropertyMatchSalesAutomationRetryIncident incident,
                DateTime? now = null)
        {
            ArgumentNullException.ThrowIfNull(incident);

            var currentTime =
                now ?? DateTime.UtcNow;

            var policy =
                GetPolicy(
                    incident.Severity);

            var acknowledgeDeadline =
                incident.CreatedAt.AddMinutes(
                    policy.AcknowledgeMinutes);

            var resolveDeadline =
                incident.CreatedAt.AddMinutes(
                    policy.ResolveMinutes);

            var acknowledgeCompletedAt =
                incident.AcknowledgedAt ??
                incident.ResolvedAt;

            var acknowledgeBreached =
                acknowledgeCompletedAt.HasValue
                    ? acknowledgeCompletedAt.Value >
                      acknowledgeDeadline
                    : currentTime >
                      acknowledgeDeadline;

            var resolveBreached =
                incident.ResolvedAt.HasValue
                    ? incident.ResolvedAt.Value >
                      resolveDeadline
                    : currentTime >
                      resolveDeadline;

            var acknowledgeOverdueMinutes =
                CalculateOverdueMinutes(
                    acknowledgeCompletedAt ??
                    currentTime,
                    acknowledgeDeadline);

            var resolveOverdueMinutes =
                CalculateOverdueMinutes(
                    incident.ResolvedAt ??
                    currentTime,
                    resolveDeadline);

            return new()
            {
                IncidentId =
                    incident.Id,

                IncidentNumber =
                    incident.IncidentNumber,

                Severity =
                    incident.Severity,

                Status =
                    incident.Status,

                AcknowledgeTargetMinutes =
                    policy.AcknowledgeMinutes,

                ResolveTargetMinutes =
                    policy.ResolveMinutes,

                AcknowledgeDeadline =
                    acknowledgeDeadline,

                ResolveDeadline =
                    resolveDeadline,

                AcknowledgeSlaBreached =
                    acknowledgeBreached,

                ResolveSlaBreached =
                    resolveBreached,

                SlaBreached =
                    acknowledgeBreached ||
                    resolveBreached,

                AcknowledgeOverdueMinutes =
                    acknowledgeBreached
                        ? acknowledgeOverdueMinutes
                        : 0,

                ResolveOverdueMinutes =
                    resolveBreached
                        ? resolveOverdueMinutes
                        : 0,

                EvaluatedAt =
                    currentTime
            };
        }

        public async Task<
            List<PropertyMatchSalesAutomationRetryIncidentSlaDto>>
            GetRecentSlaAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            var incidents =
                await _incidentRepository
                    .GetRecentAsync(
                        limit,
                        cancellationToken);

            var now =
                DateTime.UtcNow;

            return incidents
                .Select(x =>
                    Evaluate(
                        x,
                        now))
                .OrderByDescending(x =>
                    x.SlaBreached)
                .ThenByDescending(x =>
                    x.ResolveOverdueMinutes)
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationRetryIncidentSlaDto>>
            GetBreachedAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            var sla =
                await GetRecentSlaAsync(
                    Math.Clamp(
                        limit * 5,
                        1,
                        1000),
                    cancellationToken);

            return sla
                .Where(x =>
                    x.SlaBreached)
                .Take(
                    Math.Clamp(
                        limit,
                        1,
                        1000))
                .ToList();
        }

        private static PropertyMatchSalesAutomationRetryIncidentSlaPolicy
            GetPolicy(
                string? severity)
        {
            if (string.Equals(
                    severity,
                    "Critical",
                    StringComparison.OrdinalIgnoreCase))
            {
                return new()
                {
                    AcknowledgeMinutes = 15,
                    ResolveMinutes = 120
                };
            }

            if (string.Equals(
                    severity,
                    "High",
                    StringComparison.OrdinalIgnoreCase))
            {
                return new()
                {
                    AcknowledgeMinutes = 30,
                    ResolveMinutes = 240
                };
            }

            return new()
            {
                AcknowledgeMinutes = 60,
                ResolveMinutes = 480
            };
        }

        private static int CalculateOverdueMinutes(
            DateTime actual,
            DateTime deadline)
        {
            if (actual <= deadline)
            {
                return 0;
            }

            return (int)Math.Ceiling(
                (actual - deadline)
                    .TotalMinutes);
        }
    }

    public class PropertyMatchSalesAutomationRetryIncidentSlaPolicy
    {
        public int AcknowledgeMinutes { get; set; }

        public int ResolveMinutes { get; set; }
    }

    public class PropertyMatchSalesAutomationRetryIncidentSlaDto
    {
        public Guid IncidentId { get; set; }

        public string IncidentNumber { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public int AcknowledgeTargetMinutes { get; set; }

        public int ResolveTargetMinutes { get; set; }

        public DateTime AcknowledgeDeadline { get; set; }

        public DateTime ResolveDeadline { get; set; }

        public bool AcknowledgeSlaBreached { get; set; }

        public bool ResolveSlaBreached { get; set; }

        public bool SlaBreached { get; set; }

        public int AcknowledgeOverdueMinutes { get; set; }

        public int ResolveOverdueMinutes { get; set; }

        public DateTime EvaluatedAt { get; set; }
    }
}
