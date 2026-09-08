using REAL_ESTATE_CLEAN.Core.Domain.Entities;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRetryIncidentService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationRetryRiskScoreService
                _riskScoreService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryIncidentRepository
                _incidentRepository;

        public PropertyCustomerMatchSalesAutomationRetryIncidentService(
            PropertyCustomerMatchSalesAutomationRetryRiskScoreService riskScoreService,
            PropertyCustomerMatchSalesAutomationRetryIncidentRepository incidentRepository)
        {
            _riskScoreService =
                riskScoreService;

            _incidentRepository =
                incidentRepository;
        }

        public async Task<PropertyMatchSalesAutomationRetryIncidentResultDto>
            EvaluateAsync(
                CancellationToken cancellationToken = default)
        {
            var risk =
                await _riskScoreService
                    .GetRiskScoreAsync(
                        cancellationToken);

            var existing =
                await _incidentRepository
                    .GetOpenIncidentAsync(
                        cancellationToken);

            var requiresIncident =
                risk.RiskLevel == "High" ||
                risk.RiskLevel == "Critical";

            if (!requiresIncident)
            {
                return new()
                {
                    IncidentRequired = false,
                    IncidentCreated = false,
                    ExistingIncident = existing,
                    Risk = risk,
                    Message =
                        existing == null
                            ? "No incident required."
                            : "Risk decreased but an existing incident remains open.",
                    EvaluatedAt = DateTime.UtcNow
                };
            }

            if (existing != null)
            {
                return new()
                {
                    IncidentRequired = true,
                    IncidentCreated = false,
                    ExistingIncident = existing,
                    Risk = risk,
                    Message =
                        "An active retry incident already exists.",
                    EvaluatedAt = DateTime.UtcNow
                };
            }

            var incident =
                new PropertyMatchSalesAutomationRetryIncident
                {
                    Id =
                        Guid.NewGuid(),

                    Title =
                        $"Retry Operations {risk.RiskLevel} Risk",

                    Severity =
                        risk.RiskLevel,

                    Status =
                        "Open",

                    RiskScore =
                        risk.RiskScore,

                    RiskLevel =
                        risk.RiskLevel,

                    Description =
                        BuildDescription(
                            risk),

                    CreatedAt =
                        DateTime.UtcNow,

                    UpdatedAt =
                        DateTime.UtcNow
                };

            incident =
                await _incidentRepository
                    .CreateAsync(
                        incident,
                        cancellationToken);

            return new()
            {
                IncidentRequired = true,
                IncidentCreated = true,
                CreatedIncident = incident,
                Risk = risk,
                Message =
                    "Retry operations incident created.",
                EvaluatedAt = DateTime.UtcNow
            };
        }

        private static string BuildDescription(
            PropertyMatchSalesAutomationRetryRiskScoreDto risk)
        {
            if (risk.Factors.Count == 0)
            {
                return
                    $"Risk score {risk.RiskScore}; level {risk.RiskLevel}.";
            }

            var factors =
                string.Join(
                    ", ",
                    risk.Factors
                        .Where(x =>
                            x.Impact > 0)
                        .OrderByDescending(x =>
                            x.Impact)
                        .Select(x =>
                            $"{x.Code} ({x.Impact})"));

            return
                $"Risk score {risk.RiskScore}; " +
                $"level {risk.RiskLevel}; " +
                $"trend {risk.Trend}; " +
                $"factors: {factors}.";
        }
    }

    public class PropertyMatchSalesAutomationRetryIncidentResultDto
    {
        public bool IncidentRequired { get; set; }

        public bool IncidentCreated { get; set; }

        public PropertyMatchSalesAutomationRetryIncident?
            CreatedIncident { get; set; }

        public PropertyMatchSalesAutomationRetryIncident?
            ExistingIncident { get; set; }

        public PropertyMatchSalesAutomationRetryRiskScoreDto?
            Risk { get; set; }

        public string Message { get; set; }
            = string.Empty;

        public DateTime EvaluatedAt { get; set; }
    }
}
