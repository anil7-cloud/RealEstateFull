namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaService
                _slaService;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository
                _breachRepository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaService slaService,
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaBreachRepository breachRepository)
        {
            _slaService =
                slaService;

            _breachRepository =
                breachRepository;
        }

        public async Task<
            PropertyMatchSalesAutomationReliabilityAlertSlaBreachProcessDto>
            ProcessAsync(
                CancellationToken cancellationToken = default)
        {
            var sla =
                await _slaService
                    .GetSummaryAsync(
                        cancellationToken);

            var created =
                0;

            var updated =
                0;

            var resolved =
                0;

            foreach (var item in sla.Alerts)
            {
                if (item.AcknowledgeSlaBreached)
                {
                    var result =
                        await _breachRepository
                            .AddIfNeededAsync(
                                item,
                                "Acknowledge",
                                item.AcknowledgeTargetMinutes,
                                item.ActualAcknowledgeMinutes,
                                cancellationToken);

                    if (result != null)
                    {
                        created++;
                    }
                    else
                    {
                        updated++;
                    }
                }

                if (item.ResolutionSlaBreached)
                {
                    var result =
                        await _breachRepository
                            .AddIfNeededAsync(
                                item,
                                "Resolution",
                                item.ResolutionTargetMinutes,
                                item.ActualResolutionMinutes,
                                cancellationToken);

                    if (result != null)
                    {
                        created++;
                    }
                    else
                    {
                        updated++;
                    }
                }

                /*
                 * Alert tamamen çözülmüşse açık SLA breach'lerini
                 * de kapat.
                 */
                if (string.Equals(
                    item.Status,
                    "Resolved",
                    StringComparison.OrdinalIgnoreCase))
                {
                    resolved +=
                        await _breachRepository
                            .ResolveByAlertAsync(
                                item.AlertId,
                                cancellationToken);
                }
            }

            return new()
            {
                EvaluatedAlerts =
                    sla.Alerts.Count,

                CreatedBreaches =
                    created,

                UpdatedBreaches =
                    updated,

                ResolvedBreaches =
                    resolved,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }
    }

    public class
        PropertyMatchSalesAutomationReliabilityAlertSlaBreachProcessDto
    {
        public int EvaluatedAlerts { get; set; }

        public int CreatedBreaches { get; set; }

        public int UpdatedBreaches { get; set; }

        public int ResolvedBreaches { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
