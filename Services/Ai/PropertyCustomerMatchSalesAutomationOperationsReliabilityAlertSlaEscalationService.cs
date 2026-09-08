namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository
                _breachRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository
                _escalationRepository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository breachRepository,
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository escalationRepository)
        {
            _breachRepository = breachRepository;
            _escalationRepository = escalationRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationReliabilityAlertSlaEscalationProcessDto>
            ProcessAsync(
                CancellationToken cancellationToken = default)
        {
            /*
             * Artık aktif olmayan breach'lere bağlı
             * escalation kayıtlarını otomatik kapat.
             */
            var resolvedEscalations =
                await _escalationRepository
                    .ResolveInactiveBreachesAsync(
                        cancellationToken);

            var breaches =
                await _breachRepository
                    .GetActiveAsync(
                        1000,
                        cancellationToken);

            var created = 0;

            foreach (var breach in breaches)
            {
                var targetLevel =
                    GetEscalationLevel(
                        breach.Priority,
                        breach.OverdueMinutes);

                if (targetLevel == null)
                {
                    continue;
                }

                /*
                 * Seviyeler sırayla kaydedilir.
                 * Örneğin doğrudan L3 şartına geldiyse
                 * L1 + L2 + L3 audit zinciri korunur.
                 */
                var levels =
                    targetLevel switch
                    {
                        "L3" => new[] { "L1", "L2", "L3" },
                        "L2" => new[] { "L1", "L2" },
                        _ => new[] { "L1" }
                    };

                foreach (var level in levels)
                {
                    var escalation =
                        await _escalationRepository
                            .CreateIfNeededAsync(
                                breach,
                                level,
                                cancellationToken);

                    if (escalation != null)
                    {
                        created++;
                    }
                }
            }

            return new()
            {
                EvaluatedBreaches =
                    breaches.Count,

                CreatedEscalations =
                    created,

                ResolvedEscalations =
                    resolvedEscalations,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static string? GetEscalationLevel(
            string priority,
            decimal overdueMinutes)
        {
            if (string.Equals(
                priority,
                "P1",
                StringComparison.OrdinalIgnoreCase))
            {
                if (overdueMinutes >= 30m)
                    return "L3";

                if (overdueMinutes >= 15m)
                    return "L2";

                if (overdueMinutes >= 5m)
                    return "L1";

                return null;
            }

            /*
             * P2 daha yavaş escalate edilir.
             */
            if (overdueMinutes >= 120m)
                return "L3";

            if (overdueMinutes >= 60m)
                return "L2";

            if (overdueMinutes >= 30m)
                return "L1";

            return null;
        }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaEscalationProcessDto
    {
        public int EvaluatedBreaches { get; set; }

        public int CreatedEscalations { get; set; }

        public int ResolvedEscalations { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
