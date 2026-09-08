namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyHistoryRepository
                _anomalyRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertRepository
                _alertRepository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAnomalyHistoryRepository anomalyRepository,
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertRepository alertRepository)
        {
            _anomalyRepository =
                anomalyRepository;

            _alertRepository =
                alertRepository;
        }

        public async Task<int>
            ProcessActiveAnomaliesAsync(
                CancellationToken cancellationToken = default)
        {
            var anomalies =
                await _anomalyRepository
                    .GetActiveAsync(
                        100,
                        cancellationToken);

            var createdOrExisting =
                0;

            foreach (var anomaly in anomalies)
            {
                if (!string.Equals(
                        anomaly.Severity,
                        "Warning",
                        StringComparison.OrdinalIgnoreCase)
                    &&
                    !string.Equals(
                        anomaly.Severity,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                await _alertRepository
                    .CreateAsync(
                        anomaly.Id,
                        anomaly.Severity,
                        anomaly.Reason,
                        anomaly.CurrentScore,
                        anomaly.ScoreDrop,
                        cancellationToken);

                createdOrExisting++;
            }

            return createdOrExisting;
        }
    }
}
