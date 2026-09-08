using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-analytics")]
    public class PropertyCustomerMatchSalesAutomationAnalyticsController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesAutomationAnalyticsService _service;

        public PropertyCustomerMatchSalesAutomationAnalyticsController(
            PropertyCustomerMatchSalesAutomationAnalyticsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<PropertyMatchSalesAutomationAnalyticsDto>>
            GetAnalytics()
        {
            var result =
                await _service.GetAnalyticsAsync();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult>
            GetSummary()
        {
            var analytics =
                await _service.GetAnalyticsAsync();

            return Ok(new
            {
                totalExecutions =
                    analytics.TotalExecutions,

                completedExecutions =
                    analytics.CompletedExecutions,

                failedExecutions =
                    analytics.FailedExecutions,

                pendingExecutions =
                    analytics.PendingExecutions,

                executingExecutions =
                    analytics.ExecutingExecutions,

                successRate =
                    analytics.SuccessRate,

                failureRate =
                    analytics.FailureRate,

                completionRate =
                    analytics.CompletionRate,

                overdueCount =
                    analytics.OverdueCount,

                healthScore =
                    analytics.HealthScore,

                healthLevel =
                    analytics.HealthLevel,

                topPerformingAction =
                    analytics.TopPerformingAction,

                highestFailureAction =
                    analytics.HighestFailureAction,

                managementInsight =
                    analytics.ManagementInsight,

                generatedAt =
                    analytics.GeneratedAt
            });
        }

        [HttpGet("actions")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationActionPerformanceDto>>>
            GetActions()
        {
            var result =
                await _service.GetActionPerformanceAsync();

            return Ok(result);
        }

        [HttpGet("channels")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationChannelPerformanceDto>>>
            GetChannels()
        {
            var result =
                await _service.GetChannelPerformanceAsync();

            return Ok(result);
        }

        [HttpGet("failures")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationExecutionDto>>>
            GetFailures(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetFailuresAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("successful")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationExecutionDto>>>
            GetSuccessful(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetSuccessfulExecutionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<
            ActionResult<
                List<PropertyMatchSalesAutomationExecutionDto>>>
            GetOverdue(
                [FromQuery] int limit = 20)
        {
            var result =
                await _service.GetOverdueAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("status-distribution")]
        public async Task<IActionResult>
            GetStatusDistribution()
        {
            var analytics =
                await _service.GetAnalyticsAsync();

            return Ok(
                analytics.StatusDistribution);
        }

        [HttpGet("urgency-performance")]
        public async Task<IActionResult>
            GetUrgencyPerformance()
        {
            var analytics =
                await _service.GetAnalyticsAsync();

            return Ok(
                analytics.UrgencyPerformance);
        }

        [HttpGet("health")]
        public async Task<IActionResult>
            GetHealth()
        {
            var analytics =
                await _service.GetAnalyticsAsync();

            return Ok(new
            {
                score =
                    analytics.HealthScore,

                level =
                    analytics.HealthLevel,

                successRate =
                    analytics.SuccessRate,

                failureRate =
                    analytics.FailureRate,

                overdueCount =
                    analytics.OverdueCount,

                insight =
                    analytics.ManagementInsight,

                generatedAt =
                    analytics.GeneratedAt
            });
        }

        [HttpGet("top-action")]
        public async Task<IActionResult>
            GetTopAction()
        {
            var actions =
                await _service.GetActionPerformanceAsync();

            var result = actions
                .OrderByDescending(
                    x => x.SuccessRate)
                .ThenByDescending(
                    x => x.AverageExecutionScore)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Aksiyon performans verisi bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("worst-action")]
        public async Task<IActionResult>
            GetWorstAction()
        {
            var actions =
                await _service.GetActionPerformanceAsync();

            var result = actions
                .OrderByDescending(
                    x => x.FailureRate)
                .ThenByDescending(
                    x => x.TotalExecutions)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Aksiyon performans verisi bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("top-channel")]
        public async Task<IActionResult>
            GetTopChannel()
        {
            var channels =
                await _service.GetChannelPerformanceAsync();

            var result = channels
                .OrderByDescending(
                    x => x.SuccessRate)
                .ThenByDescending(
                    x => x.TotalExecutions)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Kanal performans verisi bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("kpi")]
        public async Task<IActionResult>
            GetKpi()
        {
            var analytics =
                await _service.GetAnalyticsAsync();

            return Ok(new
            {
                execution = new
                {
                    total =
                        analytics.TotalExecutions,

                    completed =
                        analytics.CompletedExecutions,

                    failed =
                        analytics.FailedExecutions,

                    overdue =
                        analytics.OverdueCount
                },

                performance = new
                {
                    successRate =
                        analytics.SuccessRate,

                    failureRate =
                        analytics.FailureRate,

                    completionRate =
                        analytics.CompletionRate,

                    averageExecutionScore =
                        analytics.AverageExecutionScore,

                    averageUrgencyScore =
                        analytics.AverageUrgencyScore
                },

                automation = new
                {
                    approvalRequired =
                        analytics.ApprovalRequiredCount,

                    autoExecutable =
                        analytics.AutoExecutableCount,

                    totalAttempts =
                        analytics.TotalAttempts,

                    averageAttempts =
                        analytics.AverageAttempts
                },

                health = new
                {
                    score =
                        analytics.HealthScore,

                    level =
                        analytics.HealthLevel
                },

                generatedAt =
                    analytics.GeneratedAt
            });
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult>
            GetDashboard()
        {
            var analyticsTask =
                _service.GetAnalyticsAsync();

            var actionsTask =
                _service.GetActionPerformanceAsync();

            var channelsTask =
                _service.GetChannelPerformanceAsync();

            var failuresTask =
                _service.GetFailuresAsync(10);

            var successfulTask =
                _service.GetSuccessfulExecutionsAsync(10);

            var overdueTask =
                _service.GetOverdueAsync(10);

            await Task.WhenAll(
                analyticsTask,
                actionsTask,
                channelsTask,
                failuresTask,
                successfulTask,
                overdueTask);

            var analytics =
                await analyticsTask;

            var actions =
                await actionsTask;

            var channels =
                await channelsTask;

            var failures =
                await failuresTask;

            var successful =
                await successfulTask;

            var overdue =
                await overdueTask;

            var topActions = actions
                .OrderByDescending(
                    x => x.SuccessRate)
                .ThenByDescending(
                    x => x.AverageExecutionScore)
                .Take(5)
                .ToList();

            var topChannels = channels
                .OrderByDescending(
                    x => x.SuccessRate)
                .ThenByDescending(
                    x => x.TotalExecutions)
                .Take(5)
                .ToList();

            return Ok(new
            {
                analytics,

                topActions,

                topChannels,

                recentFailures =
                    failures,

                recentSuccessful =
                    successful,

                overdue,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("service-health")]
        public IActionResult ServiceHealth()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesAutomationAnalytics",

                status =
                    "Running",

                timestamp =
                    DateTime.UtcNow
            });
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
