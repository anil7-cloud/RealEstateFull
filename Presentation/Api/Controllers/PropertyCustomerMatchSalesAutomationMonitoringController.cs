using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-monitoring")]
    public class PropertyCustomerMatchSalesAutomationMonitoringController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationMonitoringService
                _service;

        public PropertyCustomerMatchSalesAutomationMonitoringController(
            PropertyCustomerMatchSalesAutomationMonitoringService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationMonitoringDto>>
            GetMonitoring()
        {
            var result =
                await _service.GetMonitoringAsync();

            return Ok(result);
        }

        [HttpGet("health")]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationMonitoringHealthDto>>
            GetHealth()
        {
            var result =
                await _service.GetHealthAsync();

            return Ok(result);
        }

        [HttpGet("alerts")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationMonitoringAlertDto>>>
            GetAlerts(
                [FromQuery] int limit = 50)
        {
            var result =
                await _service.GetAlertsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("critical-alerts")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationMonitoringAlertDto>>>
            GetCriticalAlerts(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetCriticalAlertsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("warnings")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationMonitoringAlertDto>>>
            GetWarnings(
                [FromQuery] int limit = 30)
        {
            var alerts =
                await _service.GetAlertsAsync(100);

            var result =
                alerts
                    .Where(x =>
                        string.Equals(
                            x.Severity,
                            "Warning",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.Score)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("info")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationMonitoringAlertDto>>>
            GetInfo(
                [FromQuery] int limit = 30)
        {
            var alerts =
                await _service.GetAlertsAsync(100);

            var result =
                alerts
                    .Where(x =>
                        string.Equals(
                            x.Severity,
                            "Info",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.CreatedAt)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("alert-type/{type}")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationMonitoringAlertDto>>>
            GetByType(
                string type,
                [FromQuery] int limit = 30)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return BadRequest(new
                {
                    message =
                        "Alert type boş olamaz."
                });
            }

            var alerts =
                await _service.GetAlertsAsync(100);

            var result =
                alerts
                    .Where(x =>
                        string.Equals(
                            x.Type,
                            type,
                            StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(
                        x => x.Score)
                    .Take(
                        NormalizeLimit(limit))
                    .ToList();

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var monitoring =
                await _service.GetMonitoringAsync();

            var critical =
                monitoring.Alerts
                    .Where(x =>
                        x.Severity == "Critical")
                    .OrderByDescending(
                        x => x.Score)
                    .Take(10)
                    .ToList();

            var warnings =
                monitoring.Alerts
                    .Where(x =>
                        x.Severity == "Warning")
                    .OrderByDescending(
                        x => x.Score)
                    .Take(10)
                    .ToList();

            return Ok(new
            {
                system = new
                {
                    status =
                        monitoring.Status,

                    monitoringScore =
                        monitoring.MonitoringScore,

                    pipelineHealthScore =
                        monitoring.PipelineHealthScore,

                    pipelineStatus =
                        monitoring.PipelineStatus
                },

                decisions = new
                {
                    total =
                        monitoring.TotalDecisions,

                    critical =
                        monitoring.CriticalDecisions,

                    highConfidence =
                        monitoring.HighConfidenceDecisions
                },

                executions = new
                {
                    total =
                        monitoring.TotalExecutions,

                    pending =
                        monitoring.PendingExecutions,

                    executing =
                        monitoring.ExecutingExecutions,

                    completed =
                        monitoring.CompletedExecutions,

                    failed =
                        monitoring.FailedExecutions,

                    overdue =
                        monitoring.OverdueExecutions
                },

                performance = new
                {
                    successRate =
                        monitoring.SuccessRate,

                    failureRate =
                        monitoring.FailureRate,

                    optimizationScore =
                        monitoring.OptimizationScore,

                    highPriorityOptimizations =
                        monitoring.HighPriorityOptimizations
                },

                alerts = new
                {
                    criticalCount =
                        monitoring.CriticalAlertCount,

                    warningCount =
                        monitoring.WarningAlertCount,

                    critical,

                    warnings
                },

                generatedAt =
                    monitoring.GeneratedAt
            });
        }

        [HttpGet("summary")]
        public async Task<IActionResult>
            GetSummary()
        {
            var monitoring =
                await _service.GetMonitoringAsync();

            return Ok(new
            {
                status =
                    monitoring.Status,

                score =
                    monitoring.MonitoringScore,

                pipelineHealth =
                    monitoring.PipelineHealthScore,

                successRate =
                    monitoring.SuccessRate,

                failureRate =
                    monitoring.FailureRate,

                totalExecutions =
                    monitoring.TotalExecutions,

                failedExecutions =
                    monitoring.FailedExecutions,

                overdueExecutions =
                    monitoring.OverdueExecutions,

                criticalAlerts =
                    monitoring.CriticalAlertCount,

                warningAlerts =
                    monitoring.WarningAlertCount,

                generatedAt =
                    monitoring.GeneratedAt
            });
        }

        [HttpGet("kpi")]
        public async Task<IActionResult>
            GetKpi()
        {
            var monitoring =
                await _service.GetMonitoringAsync();

            var completionRate =
                Percentage(
                    monitoring.CompletedExecutions,
                    monitoring.TotalExecutions);

            var pendingRate =
                Percentage(
                    monitoring.PendingExecutions,
                    monitoring.TotalExecutions);

            var criticalDecisionRate =
                Percentage(
                    monitoring.CriticalDecisions,
                    monitoring.TotalDecisions);

            return Ok(new
            {
                health = new
                {
                    monitoringScore =
                        monitoring.MonitoringScore,

                    pipelineHealthScore =
                        monitoring.PipelineHealthScore,

                    status =
                        monitoring.Status
                },

                execution = new
                {
                    completionRate,

                    pendingRate,

                    successRate =
                        monitoring.SuccessRate,

                    failureRate =
                        monitoring.FailureRate,

                    overdue =
                        monitoring.OverdueExecutions
                },

                decisions = new
                {
                    total =
                        monitoring.TotalDecisions,

                    critical =
                        monitoring.CriticalDecisions,

                    criticalDecisionRate,

                    highConfidence =
                        monitoring.HighConfidenceDecisions
                },

                optimization = new
                {
                    score =
                        monitoring.OptimizationScore,

                    highPriority =
                        monitoring.HighPriorityOptimizations
                },

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("system-status")]
        public async Task<IActionResult>
            GetSystemStatus()
        {
            var health =
                await _service.GetHealthAsync();

            var operational =
                health.Healthy &&
                health.CriticalAlerts == 0;

            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationMonitoring",

                operational,

                status =
                    health.Status,

                healthScore =
                    health.Score,

                criticalAlerts =
                    health.CriticalAlerts,

                warningAlerts =
                    health.WarningAlerts,

                failureRate =
                    health.FailureRate,

                overdueExecutions =
                    health.OverdueExecutions,

                timestamp =
                    DateTime.UtcNow
            });
        }

        [HttpGet("ready")]
        public async Task<IActionResult>
            Ready()
        {
            var health =
                await _service.GetHealthAsync();

            if (!health.Healthy)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    new
                    {
                        ready = false,

                        status =
                            health.Status,

                        score =
                            health.Score,

                        criticalAlerts =
                            health.CriticalAlerts,

                        timestamp =
                            DateTime.UtcNow
                    });
            }

            return Ok(new
            {
                ready = true,

                status =
                    health.Status,

                score =
                    health.Score,

                timestamp =
                    DateTime.UtcNow
            });
        }

        [HttpGet("service-health")]
        public IActionResult ServiceHealth()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationMonitoring",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
            });
        }

        private static decimal Percentage(
            int value,
            int total)
        {
            if (total <= 0)
                return 0;

            return Math.Round(
                ((decimal)value / total) * 100m,
                2);
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(
                limit,
                100);
        }
    }
}
