namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsHealthIncidentService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthTrendAnomalyService
                _anomalyService;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsHealthIncidentService(
            PropertyCustomerMatchSalesAutomationOperationsHealthTrendAnomalyService anomalyService,
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository repository)
        {
            _anomalyService =
                anomalyService;

            _repository =
                repository;
        }

        public async Task<
            PropertyMatchSalesAutomationOperationsHealthIncidentProcessDto>
            ProcessAsync(
                CancellationToken cancellationToken = default)
        {
            var anomaly =
                await _anomalyService
                    .DetectAsync(
                        cancellationToken);

            var active =
                await _repository
                    .GetActiveAsync(
                        cancellationToken);

            /*
             * Anomaly kalmadıysa açık incident'i kapat.
             */
            if (!anomaly.HasAnomaly)
            {
                var resolved =
                    await _repository
                        .ResolveActiveAsync(
                            cancellationToken);

                return new()
                {
                    Action =
                        resolved
                            ? "Resolved"
                            : "None",

                    HasAnomaly =
                        false,

                    Severity =
                        "None",

                    GeneratedAt =
                        DateTime.UtcNow
                };
            }

            /*
             * Aktif incident varsa yeni satır oluşturma.
             * Mevcut incident'i güncelle.
             */
            if (active != null)
            {
                await _repository
                    .UpdateAsync(
                        active,
                        anomaly,
                        cancellationToken);

                return new()
                {
                    IncidentId =
                        active.Id,

                    IncidentNumber =
                        active.IncidentNumber,

                    Action =
                        "Updated",

                    HasAnomaly =
                        true,

                    Severity =
                        anomaly.Severity,

                    ScoreDrop =
                        anomaly.ScoreDrop,

                    GeneratedAt =
                        DateTime.UtcNow
                };
            }

            var created =
                await _repository
                    .CreateAsync(
                        anomaly,
                        cancellationToken);

            return new()
            {
                IncidentId =
                    created.Id,

                IncidentNumber =
                    created.IncidentNumber,

                Action =
                    "Created",

                HasAnomaly =
                    true,

                Severity =
                    anomaly.Severity,

                ScoreDrop =
                    anomaly.ScoreDrop,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }
    }

    public class
        PropertyMatchSalesAutomationOperationsHealthIncidentProcessDto
    {
        public Guid? IncidentId { get; set; }

        public string? IncidentNumber { get; set; }

        public string Action { get; set; }
            = "None";

        public bool HasAnomaly { get; set; }

        public string Severity { get; set; }
            = "None";

        public decimal ScoreDrop { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
