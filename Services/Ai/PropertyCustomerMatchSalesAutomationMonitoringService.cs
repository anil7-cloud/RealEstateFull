namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationMonitoringService
    {
        private readonly PropertyCustomerMatchSalesAutomationPipelineService
            _pipelineService;

        public PropertyCustomerMatchSalesAutomationMonitoringService(
            PropertyCustomerMatchSalesAutomationPipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        public async Task<PropertyMatchSalesAutomationMonitoringDto>
            GetMonitoringAsync()
        {
            var health =
                await _pipelineService.GetHealthAsync();

            var dashboard =
                await _pipelineService.GetDashboardAsync();

            var alerts =
                new List<PropertyMatchSalesAutomationMonitoringAlertDto>();

            foreach (var issue in health.Issues)
            {
                alerts.Add(new PropertyMatchSalesAutomationMonitoringAlertDto
                {
                    AlertId = Guid.NewGuid(),
                    Type = "Pipeline",
                    Severity =
                        health.HealthScore < 40
                            ? "Critical"
                            : "Warning",
                    Score =
                        Math.Clamp(
                            100m - health.HealthScore,
                            0m,
                            100m),
                    Title = "Pipeline uyarısı",
                    Message = issue,
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (alerts.Count == 0)
            {
                alerts.Add(new PropertyMatchSalesAutomationMonitoringAlertDto
                {
                    AlertId = Guid.NewGuid(),
                    Type = "System",
                    Severity = "Info",
                    Score = 10,
                    Title = "Sistem normal",
                    Message =
                        "Kritik bir pipeline problemi tespit edilmedi.",
                    CreatedAt = DateTime.UtcNow
                });
            }

            return new PropertyMatchSalesAutomationMonitoringDto
            {
                Status = health.Status,

                MonitoringScore =
                    health.HealthScore,

                PipelineHealthScore =
                    health.HealthScore,

                PipelineStatus =
                    health.Status,

                TotalDecisions =
                    dashboard.DecisionSummary.TotalDecisions,

                CriticalDecisions =
                    dashboard.DecisionSummary.CriticalDecisions,

                HighConfidenceDecisions =
                    dashboard.DecisionSummary.HighConfidenceDecisions,

                TotalExecutions =
                    dashboard.ExecutionSummary.TotalExecutions,

                PendingExecutions =
                    dashboard.ExecutionSummary.PendingExecutions,

                ExecutingExecutions =
                    dashboard.ExecutionSummary.ExecutingExecutions,

                CompletedExecutions =
                    dashboard.ExecutionSummary.CompletedExecutions,

                FailedExecutions =
                    dashboard.ExecutionSummary.FailedExecutions,

                OverdueExecutions =
                    dashboard.Analytics.OverdueCount,

                SuccessRate =
                    dashboard.Analytics.SuccessRate,

                FailureRate =
                    dashboard.Analytics.FailureRate,

                OptimizationScore =
                    dashboard.Optimization.OptimizationScore,

                HighPriorityOptimizations =
                    dashboard.Optimization.HighPriorityChanges,

                CriticalAlertCount =
                    alerts.Count(x =>
                        x.Severity == "Critical"),

                WarningAlertCount =
                    alerts.Count(x =>
                        x.Severity == "Warning"),

                Alerts =
                    alerts,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<
            List<PropertyMatchSalesAutomationMonitoringAlertDto>>
            GetAlertsAsync(int limit = 50)
        {
            var monitoring =
                await GetMonitoringAsync();

            return monitoring.Alerts
                .OrderByDescending(x =>
                    SeverityOrder(x.Severity))
                .ThenByDescending(x =>
                    x.Score)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationMonitoringAlertDto>>
            GetCriticalAlertsAsync(int limit = 20)
        {
            var alerts =
                await GetAlertsAsync(100);

            return alerts
                .Where(x =>
                    string.Equals(
                        x.Severity,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x =>
                    x.Score)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            PropertyMatchSalesAutomationMonitoringHealthDto>
            GetHealthAsync()
        {
            var monitoring =
                await GetMonitoringAsync();

            return new PropertyMatchSalesAutomationMonitoringHealthDto
            {
                Score =
                    monitoring.MonitoringScore,

                Status =
                    monitoring.Status,

                Healthy =
                    monitoring.MonitoringScore >= 70 &&
                    monitoring.CriticalAlertCount == 0,

                CriticalAlerts =
                    monitoring.CriticalAlertCount,

                WarningAlerts =
                    monitoring.WarningAlertCount,

                FailureRate =
                    monitoring.FailureRate,

                OverdueExecutions =
                    monitoring.OverdueExecutions,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static int SeverityOrder(
            string severity)
        {
            return severity switch
            {
                "Critical" => 4,
                "Warning" => 3,
                "Info" => 2,
                _ => 1
            };
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }

    public class PropertyMatchSalesAutomationMonitoringDto
    {
        public string Status { get; set; }
            = string.Empty;

        public decimal MonitoringScore { get; set; }

        public decimal PipelineHealthScore { get; set; }

        public string PipelineStatus { get; set; }
            = string.Empty;

        public int TotalDecisions { get; set; }

        public int CriticalDecisions { get; set; }

        public int HighConfidenceDecisions { get; set; }

        public int TotalExecutions { get; set; }

        public int PendingExecutions { get; set; }

        public int ExecutingExecutions { get; set; }

        public int CompletedExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public int OverdueExecutions { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public decimal OptimizationScore { get; set; }

        public int HighPriorityOptimizations { get; set; }

        public int CriticalAlertCount { get; set; }

        public int WarningAlertCount { get; set; }

        public List<PropertyMatchSalesAutomationMonitoringAlertDto>
            Alerts { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationMonitoringAlertDto
    {
        public Guid AlertId { get; set; }

        public string Type { get; set; }
            = string.Empty;

        public string Severity { get; set; }
            = string.Empty;

        public decimal Score { get; set; }

        public string Title { get; set; }
            = string.Empty;

        public string Message { get; set; }
            = string.Empty;

        public Guid? ExecutionId { get; set; }

        public int? MatchId { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationMonitoringHealthDto
    {
        public decimal Score { get; set; }

        public string Status { get; set; }
            = string.Empty;

        public bool Healthy { get; set; }

        public int CriticalAlerts { get; set; }

        public int WarningAlerts { get; set; }

        public decimal FailureRate { get; set; }

        public int OverdueExecutions { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
