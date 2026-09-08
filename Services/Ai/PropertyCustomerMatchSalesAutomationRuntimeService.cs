using System.Diagnostics;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.AI;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationRuntimeService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationExecutionService
                _executionService;

        private readonly
            PropertyCustomerMatchSalesAutomationAuditIntegrationService
                _auditService;

        public PropertyCustomerMatchSalesAutomationRuntimeService(
            PropertyCustomerMatchSalesAutomationExecutionService executionService,
            PropertyCustomerMatchSalesAutomationAuditIntegrationService auditService)
        {
            _executionService = executionService;
            _auditService = auditService;
        }

        public async Task<PropertyCustomerMatchSalesAutomationRuntimeDto>
            RunAsync(
                PropertyCustomerMatchSalesAutomationRuntimeRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (request.MatchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(request.MatchId));
            }

            var stopwatch =
                Stopwatch.StartNew();

            var runtime =
                new PropertyCustomerMatchSalesAutomationRuntimeDto
                {
                    RuntimeId = Guid.NewGuid(),
                    MatchId = request.MatchId,
                    LeadId = request.LeadId,
                    PropertyId = request.PropertyId,
                    Status = "Running",
                    CurrentStage = "CreateExecution",
                    StartedAt = DateTime.UtcNow
                };

            try
            {
                await AddStageAsync(
                    runtime,
                    "CreateExecution",
                    async () =>
                    {
                        var execution =
                            await _executionService
                                .CreateForMatchAsync(
                                    request.MatchId);

                        if (execution == null)
                        {
                            throw new InvalidOperationException(
                                "Match için automation execution oluşturulamadı.");
                        }

                        runtime.ExecutionId =
                            execution.ExecutionId;

                        runtime.LeadId =
                            execution.LeadId;

                        runtime.PropertyId =
                            execution.PropertyId;

                        runtime.ExecutionCreated =
                            true;

                        runtime.MatchScore =
                            execution.ExecutionScore;

                        runtime.SelectedAction =
                            execution.Action;

                        if (request.PersistAudit)
                        {
                            await WriteAuditAsync(
                                request,
                                runtime,
                                execution,
                                "ExecutionCreated",
                                "Created",
                                true);
                        }
                    });

                var current =
                    await GetRequiredExecutionAsync(
                        runtime.ExecutionId);

                if (current.RequiresApproval)
                {
                    runtime.CurrentStage =
                        "Approval";

                    if (!request.RequireApproval)
                    {
                        runtime.Status =
                            "AwaitingApproval";

                        runtime.Successful =
                            false;

                        if (request.PersistAudit)
                        {
                            await WriteAuditAsync(
                                request,
                                runtime,
                                current,
                                "ApprovalRequired",
                                "Pending",
                                true);
                        }

                        return Finish(
                            runtime,
                            stopwatch);
                    }

                    await AddStageAsync(
                        runtime,
                        "Approval",
                        async () =>
                        {
                            var approved =
                                await _executionService
                                    .ApproveAsync(
                                        current.ExecutionId,
                                        request.Actor);

                            if (approved == null)
                            {
                                throw new InvalidOperationException(
                                    "Execution onaylanamadı.");
                            }

                            if (approved.Status != "Approved")
                            {
                                throw new InvalidOperationException(
                                    $"Beklenmeyen approval status: {approved.Status}");
                            }

                            if (request.PersistAudit)
                            {
                                await WriteAuditAsync(
                                    request,
                                    runtime,
                                    approved,
                                    "ExecutionApproved",
                                    approved.Status,
                                    true);
                            }
                        });
                }

                if (!request.ExecuteImmediately)
                {
                    runtime.Status =
                        current.RequiresApproval
                            ? "Approved"
                            : "Pending";

                    runtime.Successful =
                        true;

                    runtime.CurrentStage =
                        "WaitingExecution";

                    return Finish(
                        runtime,
                        stopwatch);
                }

                await AddStageAsync(
                    runtime,
                    "StartExecution",
                    async () =>
                    {
                        var started =
                            await _executionService
                                .StartAsync(
                                    runtime.ExecutionId!.Value);

                        if (started == null)
                        {
                            throw new InvalidOperationException(
                                "Execution başlatılamadı.");
                        }

                        if (started.Status != "Executing")
                        {
                            throw new InvalidOperationException(
                                string.IsNullOrWhiteSpace(
                                    started.LastError)
                                    ? $"Execution başlatılamadı. Status: {started.Status}"
                                    : started.LastError);
                        }

                        runtime.ExecutionStarted =
                            true;

                        if (request.PersistAudit)
                        {
                            await WriteAuditAsync(
                                request,
                                runtime,
                                started,
                                "ExecutionStarted",
                                started.Status,
                                true);
                        }
                    });

                await AddStageAsync(
                    runtime,
                    "CompleteExecution",
                    async () =>
                    {
                        var completed =
                            await _executionService
                                .CompleteAsync(
                                    runtime.ExecutionId!.Value,
                                    "Runtime execution completed successfully.");

                        if (completed == null)
                        {
                            throw new InvalidOperationException(
                                "Execution tamamlanamadı.");
                        }

                        if (completed.Status != "Completed")
                        {
                            throw new InvalidOperationException(
                                $"Execution Completed durumuna geçemedi. Status: {completed.Status}");
                        }

                        runtime.ExecutionCompleted =
                            true;

                        if (request.PersistAudit)
                        {
                            await WriteAuditAsync(
                                request,
                                runtime,
                                completed,
                                "ExecutionCompleted",
                                completed.Status,
                                true);

                            runtime.AuditPersisted =
                                true;
                        }
                    });

                runtime.Status =
                    "Completed";

                runtime.CurrentStage =
                    "Completed";

                runtime.Successful =
                    true;

                return Finish(
                    runtime,
                    stopwatch);
            }
            catch (Exception ex)
            {
                runtime.Status =
                    "Failed";

                runtime.Successful =
                    false;

                runtime.FailureStage =
                    runtime.CurrentStage;

                runtime.ErrorCode =
                    ex.GetType().Name;

                runtime.ErrorMessage =
                    ex.Message;

                if (runtime.ExecutionId.HasValue)
                {
                    try
                    {
                        var failed =
                            await _executionService
                                .FailAsync(
                                    runtime.ExecutionId.Value,
                                    ex.Message);

                        if (request.PersistAudit &&
                            failed != null)
                        {
                            await WriteAuditAsync(
                                request,
                                runtime,
                                failed,
                                "ExecutionFailed",
                                "Failed",
                                false,
                                ex);
                        }
                    }
                    catch
                    {
                        // Runtime'ın asıl hatasını koru.
                    }
                }

                return Finish(
                    runtime,
                    stopwatch);
            }
        }

        public async Task<PropertyMatchSalesAutomationExecutionDto?>
            GetExecutionAsync(
                Guid executionId)
        {
            return await _executionService
                .GetExecutionAsync(executionId);
        }

        public async Task<PropertyMatchSalesAutomationExecutionDto?>
            RetryAsync(
                Guid executionId,
                string actor = "System")
        {
            var execution =
                await _executionService
                    .RetryAsync(executionId);

            if (execution == null)
                return null;

            await _auditService.WriteAsync(
                eventType: "Runtime",
                action: "ExecutionRetry",
                status: execution.Status,
                message: "Execution retry için hazırlandı.",
                matchId: execution.MatchId,
                leadId: execution.LeadId,
                propertyId: execution.PropertyId,
                executionId: execution.ExecutionId,
                actor: actor,
                score: execution.ExecutionScore,
                isSuccessful: true);

            return execution;
        }

        public async Task<PropertyCustomerMatchSalesAutomationRuntimeHealthDto>
            GetHealthAsync()
        {
            var executionSummary =
                await _executionService
                    .GetSummaryAsync();

            var auditHealth =
                await _auditService
                    .GetStorageHealthAsync();

            var healthy =
                auditHealth.MemoryAvailable;

            return new
                PropertyCustomerMatchSalesAutomationRuntimeHealthDto
                {
                    Healthy = healthy,

                    Status =
                        healthy
                            ? "Healthy"
                            : "Degraded",

                    DecisionServiceAvailable =
                        true,

                    ExecutionServiceAvailable =
                        true,

                    AuditServiceAvailable =
                        auditHealth.MemoryAvailable,

                    DatabaseAvailable =
                        auditHealth.PersistentAvailable,

                    ActiveExecutions =
                        executionSummary.PendingExecutions +
                        executionSummary.ApprovedExecutions +
                        executionSummary.ExecutingExecutions,

                    SuccessfulExecutions =
                        executionSummary.CompletedExecutions,

                    FailedExecutions =
                        executionSummary.FailedExecutions,

                    CheckedAt =
                        DateTime.UtcNow
                };
        }

        private async Task<PropertyMatchSalesAutomationExecutionDto>
            GetRequiredExecutionAsync(
                Guid? executionId)
        {
            if (!executionId.HasValue)
            {
                throw new InvalidOperationException(
                    "ExecutionId oluşturulmadı.");
            }

            var execution =
                await _executionService
                    .GetExecutionAsync(
                        executionId.Value);

            return execution
                ?? throw new InvalidOperationException(
                    "Execution bulunamadı.");
        }

        private async Task AddStageAsync(
            PropertyCustomerMatchSalesAutomationRuntimeDto runtime,
            string stageName,
            Func<Task> action)
        {
            var stopwatch =
                Stopwatch.StartNew();

            var stage =
                new PropertyCustomerMatchSalesAutomationRuntimeStageDto
                {
                    Stage = stageName,
                    Status = "Running",
                    StartedAt = DateTime.UtcNow
                };

            runtime.CurrentStage =
                stageName;

            runtime.Stages.Add(stage);

            try
            {
                await action();

                stopwatch.Stop();

                stage.Status =
                    "Completed";

                stage.Successful =
                    true;

                stage.CompletedAt =
                    DateTime.UtcNow;

                stage.DurationMilliseconds =
                    stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                stage.Status =
                    "Failed";

                stage.Successful =
                    false;

                stage.Error =
                    ex.Message;

                stage.CompletedAt =
                    DateTime.UtcNow;

                stage.DurationMilliseconds =
                    stopwatch.ElapsedMilliseconds;

                throw;
            }
        }

        private async Task WriteAuditAsync(
            PropertyCustomerMatchSalesAutomationRuntimeRequestDto request,
            PropertyCustomerMatchSalesAutomationRuntimeDto runtime,
            PropertyMatchSalesAutomationExecutionDto execution,
            string action,
            string status,
            bool successful,
            Exception? exception = null)
        {
            var audit =
                await _auditService.WriteAsync(
                    eventType: "Runtime",
                    action: action,
                    status: status,
                    message:
                        exception?.Message ??
                        $"Runtime {action}.",
                    matchId:
                        execution.MatchId,
                    leadId:
                        execution.LeadId,
                    propertyId:
                        execution.PropertyId,
                    executionId:
                        execution.ExecutionId,
                    actor:
                        request.Actor,
                    score:
                        execution.ExecutionScore,
                    metadata:
                        $"RuntimeId={runtime.RuntimeId};CorrelationId={request.CorrelationId}",
                    isSuccessful:
                        successful,
                    errorCode:
                        exception?.GetType().Name,
                    errorMessage:
                        exception?.Message);

            runtime.AuditPersisted =
                runtime.AuditPersisted ||
                audit.PersistentSucceeded;
        }

        private static PropertyCustomerMatchSalesAutomationRuntimeDto
            Finish(
                PropertyCustomerMatchSalesAutomationRuntimeDto runtime,
                Stopwatch stopwatch)
        {
            stopwatch.Stop();

            runtime.CompletedAt =
                DateTime.UtcNow;

            runtime.DurationMilliseconds =
                stopwatch.ElapsedMilliseconds;

            return runtime;
        }
    }
}
