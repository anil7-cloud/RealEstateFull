namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentEscalationService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentSlaService
                _slaService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationRetryIncidentEscalationService(
            PropertyCustomerMatchSalesAutomationRetryIncidentSlaService slaService,
            PropertyCustomerMatchSalesAutomationRetryIncidentEscalationRepository repository)
        {
            _slaService =
                slaService;

            _repository =
                repository;
        }

        public async Task<List<
            PropertyMatchSalesAutomationRetryIncidentEscalationDto>>
            GetEscalationsAsync(
                int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            var breaches =
                await _slaService
                    .GetBreachedAsync(
                        limit,
                        cancellationToken);

            var escalations =
                breaches
                    .Select(BuildEscalation)
                    .OrderByDescending(x =>
                        x.LevelNumber)
                    .ThenByDescending(x =>
                        x.OverdueMinutes)
                    .ToList();

            foreach (var escalation in escalations)
            {
                await _repository
                    .UpsertAsync(
                        new Core.Domain.Entities
                            .PropertyMatchSalesAutomationRetryIncidentEscalation
                        {
                            IncidentId =
                                escalation.IncidentId,

                            IncidentNumber =
                                escalation.IncidentNumber,

                            Severity =
                                escalation.Severity,

                            Level =
                                escalation.Level,

                            LevelNumber =
                                escalation.LevelNumber,

                            OverdueMinutes =
                                escalation.OverdueMinutes,

                            Status =
                                "Active",

                            RecommendedAction =
                                escalation.RecommendedAction,

                            CreatedBy =
                                "System"
                        },
                        cancellationToken);
            }

            return escalations;
        }

        private static
            PropertyMatchSalesAutomationRetryIncidentEscalationDto
            BuildEscalation(
                PropertyMatchSalesAutomationRetryIncidentSlaDto sla)
        {
            var overdue =
                Math.Max(
                    sla.AcknowledgeOverdueMinutes,
                    sla.ResolveOverdueMinutes);

            var level =
                GetLevel(
                    overdue,
                    sla.Severity);

            return new()
            {
                IncidentId =
                    sla.IncidentId,

                IncidentNumber =
                    sla.IncidentNumber,

                Severity =
                    sla.Severity,

                Status =
                    sla.Status,

                Level =
                    $"Level{level}",

                LevelNumber =
                    level,

                OverdueMinutes =
                    overdue,

                AcknowledgeSlaBreached =
                    sla.AcknowledgeSlaBreached,

                ResolveSlaBreached =
                    sla.ResolveSlaBreached,

                RecommendedAction =
                    GetRecommendedAction(
                        level),

                EvaluatedAt =
                    DateTime.UtcNow
            };
        }

        private static int GetLevel(
            int overdueMinutes,
            string? severity)
        {
            var critical =
                string.Equals(
                    severity,
                    "Critical",
                    StringComparison.OrdinalIgnoreCase);

            if (critical)
            {
                if (overdueMinutes >= 120)
                    return 3;

                if (overdueMinutes >= 30)
                    return 2;

                return 1;
            }

            if (overdueMinutes >= 240)
                return 3;

            if (overdueMinutes >= 60)
                return 2;

            return 1;
        }

        private static string GetRecommendedAction(
            int level)
        {
            return level switch
            {
                3 =>
                    "Immediate senior escalation and incident command required.",

                2 =>
                    "Escalate to operations lead and prioritize remediation.",

                _ =>
                    "Notify responsible operator and begin remediation."
            };
        }
    }

    public class PropertyMatchSalesAutomationRetryIncidentEscalationDto
    {
        public Guid IncidentId { get; set; }

        public string IncidentNumber { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public string Level { get; set; }
            = "Level1";

        public int LevelNumber { get; set; }

        public int OverdueMinutes { get; set; }

        public bool AcknowledgeSlaBreached { get; set; }

        public bool ResolveSlaBreached { get; set; }

        public string RecommendedAction { get; set; }
            = string.Empty;

        public DateTime EvaluatedAt { get; set; }
    }
}
