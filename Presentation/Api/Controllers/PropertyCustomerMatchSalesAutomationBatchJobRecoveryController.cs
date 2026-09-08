using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-automation-batch-recovery")]
    public class PropertyCustomerMatchSalesAutomationBatchJobRecoveryController
        : ControllerBase
    {

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository
                _operationsHealthIncidentRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentService
                _operationsHealthIncidentService;

        private readonly
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentAnalyticsService
                _operationsHealthIncidentAnalyticsService;

        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobRecoveryService
                _recoveryService;

        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobRepository
                _repository;

        private readonly
            PropertyCustomerMatchSalesAutomationBatchJobRetryService
                _retryService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryMetricsService
                _retryMetrics;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryEventRepository
                _retryEventRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryHealthService
                _retryHealthService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryDashboardService
                _retryDashboardService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertService
                _retryAlertService;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertRepository
                _retryAlertRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertHistoryRepository
                _retryAlertHistoryRepository;

        private readonly
            PropertyCustomerMatchSalesAutomationRetryAlertAnalyticsService
                _retryAlertAnalyticsService;

        public PropertyCustomerMatchSalesAutomationBatchJobRecoveryController(
            PropertyCustomerMatchSalesAutomationBatchJobRecoveryService recoveryService,
            PropertyCustomerMatchSalesAutomationBatchJobRepository repository,
            PropertyCustomerMatchSalesAutomationBatchJobRetryService retryService,
            PropertyCustomerMatchSalesAutomationRetryMetricsService retryMetrics,
            PropertyCustomerMatchSalesAutomationRetryEventRepository retryEventRepository,
            PropertyCustomerMatchSalesAutomationRetryHealthService retryHealthService,
            PropertyCustomerMatchSalesAutomationRetryDashboardService retryDashboardService,
            PropertyCustomerMatchSalesAutomationRetryAlertService retryAlertService,
            PropertyCustomerMatchSalesAutomationRetryAlertRepository retryAlertRepository,
            PropertyCustomerMatchSalesAutomationRetryAlertHistoryRepository retryAlertHistoryRepository,
            PropertyCustomerMatchSalesAutomationRetryAlertAnalyticsService retryAlertAnalyticsService,
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentRepository operationsHealthIncidentRepository,
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentService operationsHealthIncidentService,
            PropertyCustomerMatchSalesAutomationOperationsHealthIncidentAnalyticsService operationsHealthIncidentAnalyticsService)
        {

            _operationsHealthIncidentRepository =
                operationsHealthIncidentRepository;

            _operationsHealthIncidentService =
                operationsHealthIncidentService;

            _operationsHealthIncidentAnalyticsService =
                operationsHealthIncidentAnalyticsService;


            _recoveryService =
                recoveryService;

            _repository =
                repository;

            _retryService =
                retryService;

            _retryMetrics =
                retryMetrics;

            _retryEventRepository =
                retryEventRepository;

            _retryHealthService =
                retryHealthService;

            _retryDashboardService =
                retryDashboardService;

            _retryAlertService =
                retryAlertService;

            _retryAlertRepository =
                retryAlertRepository;

            _retryAlertHistoryRepository =
                retryAlertHistoryRepository;

            _retryAlertAnalyticsService =
                retryAlertAnalyticsService;
        }

        [HttpPost("run")]
        public async Task<IActionResult>
            Run(
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _recoveryService
                        .RecoverAsync(
                            cancellationToken);

                return result.Status switch
                {
                    "Completed" =>
                        Ok(result),

                    "PartiallyCompleted" =>
                        StatusCode(
                            StatusCodes.Status207MultiStatus,
                            result),

                    "DatabaseUnavailable" =>
                        StatusCode(
                            StatusCodes.Status503ServiceUnavailable,
                            result),

                    "Failed" =>
                        StatusCode(
                            StatusCodes.Status500InternalServerError,
                            result),

                    _ =>
                        Ok(result)
                };
            }
            catch (OperationCanceledException)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new
                    {
                        status =
                            "Cancelled",

                        message =
                            "Recovery işlemi iptal edildi.",

                        timestamp =
                            DateTime.UtcNow
                    });
            }
        }

        [HttpGet("interrupted")]
        public async Task<IActionResult>
            GetInterrupted(
                [FromQuery]
                int limit = 100,

                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            var jobs =
                await _repository
                    .GetByStatusAsync(
                        "Interrupted",
                        limit,
                        cancellationToken);

            return Ok(new
            {
                count =
                    jobs.Count,

                jobs,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("running")]
        public async Task<IActionResult>
            GetRunning(
                [FromQuery]
                int limit = 100,

                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            var jobs =
                await _repository
                    .GetByStatusAsync(
                        "Running",
                        limit,
                        cancellationToken);

            return Ok(new
            {
                count =
                    jobs.Count,

                jobs,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public async Task<IActionResult>
            Health(
                CancellationToken cancellationToken)
        {
            var canConnect =
                await _repository
                    .CanConnectAsync(
                        cancellationToken);

            if (!canConnect)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    new
                    {
                        healthy =
                            false,

                        databaseAvailable =
                            false,

                        status =
                            "DatabaseUnavailable",

                        checkedAt =
                            DateTime.UtcNow
                    });
            }

            var running =
                await _repository
                    .GetByStatusAsync(
                        "Running",
                        1000,
                        cancellationToken);

            var interrupted =
                await _repository
                    .GetByStatusAsync(
                        "Interrupted",
                        1000,
                        cancellationToken);

            var healthy =
                running.Count == 0;

            return Ok(new
            {
                healthy,

                databaseAvailable =
                    true,

                runningJobs =
                    running.Count,

                interruptedJobs =
                    interrupted.Count,

                status =
                    healthy
                        ? "Healthy"
                        : "RunningJobsDetected",

                checkedAt =
                    DateTime.UtcNow
            });
        }


        [HttpPost("{jobId:guid}/retry")]
        public async Task<IActionResult>
            Retry(
                Guid jobId,
                CancellationToken cancellationToken)
        {
            try
            {
                var result =
                    await _retryService
                        .RetryAsync(
                            jobId,
                            cancellationToken);

                return result.Status switch
                {
                    "Retried" =>
                        Ok(result),

                    "NotFound" =>
                        NotFound(result),

                    "NotRetryable" =>
                        Conflict(result),

                    "RetryLimitExceeded" =>
                        Conflict(result),

                    "RetryBackoffActive" =>
                        StatusCode(
                            StatusCodes.Status429TooManyRequests,
                            result),

                    "AlreadySuccessfullyRetried" =>
                        Conflict(result),

                    "RequestUnavailable" =>
                        UnprocessableEntity(result),

                    "InvalidRequestJson" =>
                        UnprocessableEntity(result),

                    "RetryFailed" =>
                        StatusCode(
                            StatusCodes.Status500InternalServerError,
                            result),

                    _ =>
                        BadRequest(result)
                };
            }
            catch (OperationCanceledException)
            {
                return StatusCode(
                    StatusCodes.Status409Conflict,
                    new
                    {
                        status = "Cancelled",
                        sourceJobId = jobId,
                        message = "Retry işlemi iptal edildi.",
                        generatedAt = DateTime.UtcNow
                    });
            }
        }


        [HttpGet("{jobId:guid}/retry-history")]
        public async Task<IActionResult>
            GetRetryHistory(
                Guid jobId,
                CancellationToken cancellationToken)
        {
            var source =
                await _repository
                    .GetByJobIdAsync(
                        jobId,
                        cancellationToken);

            if (source == null)
            {
                return NotFound(new
                {
                    status = "NotFound",
                    jobId,
                    message = "Batch job bulunamadı.",
                    generatedAt = DateTime.UtcNow
                });
            }

            var history =
                await _repository
                    .GetRetryHistoryAsync(
                        jobId,
                        cancellationToken);

            return Ok(new
            {
                requestedJobId =
                    jobId,

                rootJobId =
                    history.FirstOrDefault()?.JobId,

                retryCount =
                    Math.Max(
                        0,
                        history.Count - 1),

                totalExecutions =
                    history.Count,

                history =
                    history.Select(x => new
                    {
                        x.JobId,
                        x.ParentJobId,
                        x.RetryCount,
                        x.BatchId,
                        x.Status,
                        x.Actor,
                        x.Processed,
                        x.Successful,
                        x.Failed,
                        x.SuccessRate,
                        x.ErrorCode,
                        x.ErrorMessage,
                        x.CreatedAt,
                        x.StartedAt,
                        x.CompletedAt,
                        x.UpdatedAt
                    }),

                generatedAt =
                    DateTime.UtcNow
            });
        }



        [HttpGet("retry-events")]
        public async Task<IActionResult>
            GetRetryEvents(
                [FromQuery] int limit = 100,
                CancellationToken cancellationToken = default)
        {
            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            var events =
                await _retryEventRepository
                    .GetRecentAsync(
                        limit,
                        cancellationToken);

            return Ok(new
            {
                count =
                    events.Count,

                events,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("{jobId:guid}/retry-events")]
        public async Task<IActionResult>
            GetRetryEventsByJob(
                Guid jobId,
                [FromQuery] int limit = 100,
                CancellationToken cancellationToken = default)
        {
            var job =
                await _repository
                    .GetByJobIdAsync(
                        jobId,
                        cancellationToken);

            if (job == null)
            {
                return NotFound(new
                {
                    status = "NotFound",
                    jobId,
                    message = "Batch job bulunamadı.",
                    generatedAt = DateTime.UtcNow
                });
            }

            limit =
                Math.Clamp(
                    limit,
                    1,
                    1000);

            var events =
                await _retryEventRepository
                    .GetByJobIdAsync(
                        jobId,
                        limit,
                        cancellationToken);

            return Ok(new
            {
                jobId,

                count =
                    events.Count,

                events,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("retry-events/summary")]
        public async Task<IActionResult>
            GetRetryEventSummary(
                CancellationToken cancellationToken)
        {
            var summary =
                await _retryEventRepository
                    .GetSummaryAsync(
                        cancellationToken);

            return Ok(summary);
        }


        [HttpGet("retry-health")]
        public async Task<IActionResult>
            GetRetryHealth(
                CancellationToken cancellationToken)
        {
            var health =
                await _retryHealthService
                    .GetHealthAsync(
                        cancellationToken);

            if (!health.DatabaseAvailable ||
                !health.EventDatabaseAvailable)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    health);
            }

            return Ok(health);
        }

        [HttpGet("retry-metrics")]
        public IActionResult GetRetryMetrics()
        {
            var metrics =
                _retryMetrics.GetSnapshot();

            return Ok(metrics);
        }

        [HttpGet("summary")]
        public async Task<IActionResult>
            Summary(
                CancellationToken cancellationToken)
        {
            var persistentSummary =
                await _repository
                    .GetSummaryAsync(
                        cancellationToken);

            var interrupted =
                await _repository
                    .GetByStatusAsync(
                        "Interrupted",
                        1000,
                        cancellationToken);

            return Ok(new
            {
                persistentSummary.TotalJobs,
                persistentSummary.RunningJobs,
                persistentSummary.CompletedJobs,
                persistentSummary.PartiallyCompletedJobs,
                persistentSummary.FailedJobs,
                persistentSummary.CancelledJobs,

                interruptedJobs =
                    interrupted.Count,

                persistentSummary.TotalProcessed,
                persistentSummary.TotalSuccessful,
                persistentSummary.TotalFailed,
                persistentSummary.SuccessRate,
                persistentSummary.FailureRate,

                generatedAt =
                    DateTime.UtcNow
            });
        }
    }
}
