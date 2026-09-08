namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOperationsReliabilityEscalationSummaryService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationService
                _escalationService;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository
                _repository;

        public PropertyCustomerMatchSalesAutomationOperationsReliabilityEscalationSummaryService(
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationService escalationService,
            PropertyCustomerMatchSalesAutomationOperationsReliabilityAlertSlaEscalationRepository repository)
        {
            _escalationService =
                escalationService;

            _repository =
                repository;
        }

        public async Task<
            PropertyMatchSalesAutomationReliabilityEscalationSummaryDto>
            GetAsync(
                CancellationToken cancellationToken = default)
        {
            await _escalationService
                .ProcessAsync(
                    cancellationToken);

            var active =
                await _repository
                    .GetActiveAsync(
                        1000,
                        cancellationToken);

            return new()
            {
                ActiveEscalations =
                    active.Count,

                L1 =
                    active.Count(x =>
                        x.EscalationLevel == "L1"),

                L2 =
                    active.Count(x =>
                        x.EscalationLevel == "L2"),

                L3 =
                    active.Count(x =>
                        x.EscalationLevel == "L3"),

                P1 =
                    active.Count(x =>
                        x.Priority == "P1"),

                P2 =
                    active.Count(x =>
                        x.Priority == "P2"),

                HighestLevel =
                    active.Any(x =>
                        x.EscalationLevel == "L3")
                        ? "L3"
                        : active.Any(x =>
                            x.EscalationLevel == "L2")
                            ? "L2"
                            : active.Any(x =>
                                x.EscalationLevel == "L1")
                                ? "L1"
                                : "None",

                GeneratedAt =
                    DateTime.UtcNow
            };
        }
    }

    public class
        PropertyMatchSalesAutomationReliabilityEscalationSummaryDto
    {
        public int ActiveEscalations { get; set; }

        public int L1 { get; set; }

        public int L2 { get; set; }

        public int L3 { get; set; }

        public int P1 { get; set; }

        public int P2 { get; set; }

        public string HighestLevel { get; set; }
            = "None";

        public DateTime GeneratedAt { get; set; }
    }
}
