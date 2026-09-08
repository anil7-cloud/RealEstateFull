using System.Collections.Concurrent;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationExecutionService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationService
                _automationService;

        private readonly ConcurrentDictionary<
            Guid,
            PropertyMatchSalesAutomationExecutionDto>
                _executions = new();

        public PropertyCustomerMatchSalesAutomationExecutionService(
            PropertyCustomerMatchSalesAutomationService
                automationService)
        {
            _automationService = automationService;
        }

        public async Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            CreatePendingExecutionsAsync(
                int limit = 50)
        {
            var automations =
                await _automationService
                    .GetAutomationsAsync(
                        NormalizeLimit(limit));

            var result =
                new List<
                    PropertyMatchSalesAutomationExecutionDto>();

            foreach (var automation in automations)
            {
                var existing =
                    _executions.Values
                        .FirstOrDefault(x =>
                            x.MatchId == automation.MatchId &&
                            x.Action == automation.Action &&
                            x.Status != "Completed" &&
                            x.Status != "Failed" &&
                            x.Status != "Cancelled");

                if (existing != null)
                {
                    result.Add(existing);
                    continue;
                }

                var execution =
                    CreateExecution(automation);

                _executions[
                    execution.ExecutionId] =
                    execution;

                result.Add(execution);
            }

            return result
                .OrderByDescending(
                    x => x.ExecutionScore)
                .ThenBy(
                    x => x.ScheduledAt)
                .ToList();
        }

        public Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            GetExecutionsAsync()
        {
            var result =
                _executions.Values
                    .OrderByDescending(
                        x => x.ExecutionScore)
                    .ThenBy(
                        x => x.ScheduledAt)
                    .ToList();

            return Task.FromResult(result);
        }

        public Task<
            PropertyMatchSalesAutomationExecutionDto?>
            GetExecutionAsync(
                Guid executionId)
        {
            _executions.TryGetValue(
                executionId,
                out var execution);

            return Task.FromResult(execution);
        }

        public async Task<
            PropertyMatchSalesAutomationExecutionDto?>
            CreateForMatchAsync(
                int matchId)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var automation =
                await _automationService
                    .GetForMatchAsync(matchId);

            if (automation == null)
                return null;

            var existing =
                _executions.Values
                    .FirstOrDefault(x =>
                        x.MatchId == matchId &&
                        x.Action == automation.Action &&
                        x.Status != "Completed" &&
                        x.Status != "Failed" &&
                        x.Status != "Cancelled");

            if (existing != null)
                return existing;

            var execution =
                CreateExecution(automation);

            _executions[
                execution.ExecutionId] =
                execution;

            return execution;
        }

        public Task<
            PropertyMatchSalesAutomationExecutionDto?>
            ApproveAsync(
                Guid executionId,
                string approvedBy = "System")
        {
            if (!_executions.TryGetValue(
                executionId,
                out var execution))
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        null);
            }

            if (execution.Status != "Pending")
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        execution);
            }

            execution.Status =
                "Approved";

            execution.ApprovedBy =
                approvedBy;

            execution.ApprovedAt =
                DateTime.UtcNow;

            execution.UpdatedAt =
                DateTime.UtcNow;

            return Task.FromResult<
                PropertyMatchSalesAutomationExecutionDto?>(
                    execution);
        }

        public Task<
            PropertyMatchSalesAutomationExecutionDto?>
            StartAsync(
                Guid executionId)
        {
            if (!_executions.TryGetValue(
                executionId,
                out var execution))
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        null);
            }

            if (execution.RequiresApproval &&
                execution.Status != "Approved")
            {
                execution.LastError =
                    "Bu otomasyon çalıştırılmadan önce onaylanmalıdır.";

                execution.UpdatedAt =
                    DateTime.UtcNow;

                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        execution);
            }

            if (!execution.RequiresApproval &&
                execution.Status != "Pending" &&
                execution.Status != "Approved")
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        execution);
            }

            execution.Status =
                "Executing";

            execution.StartedAt =
                DateTime.UtcNow;

            execution.AttemptCount++;

            execution.LastError =
                string.Empty;

            execution.UpdatedAt =
                DateTime.UtcNow;

            return Task.FromResult<
                PropertyMatchSalesAutomationExecutionDto?>(
                    execution);
        }

        public Task<
            PropertyMatchSalesAutomationExecutionDto?>
            CompleteAsync(
                Guid executionId,
                string result = "")
        {
            if (!_executions.TryGetValue(
                executionId,
                out var execution))
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        null);
            }

            if (execution.Status != "Executing")
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        execution);
            }

            execution.Status =
                "Completed";

            execution.Result =
                result;

            execution.CompletedAt =
                DateTime.UtcNow;

            execution.UpdatedAt =
                DateTime.UtcNow;

            return Task.FromResult<
                PropertyMatchSalesAutomationExecutionDto?>(
                    execution);
        }

        public Task<
            PropertyMatchSalesAutomationExecutionDto?>
            FailAsync(
                Guid executionId,
                string error)
        {
            if (!_executions.TryGetValue(
                executionId,
                out var execution))
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        null);
            }

            execution.Status =
                "Failed";

            execution.LastError =
                string.IsNullOrWhiteSpace(error)
                    ? "Bilinmeyen otomasyon hatası."
                    : error;

            execution.FailedAt =
                DateTime.UtcNow;

            execution.UpdatedAt =
                DateTime.UtcNow;

            return Task.FromResult<
                PropertyMatchSalesAutomationExecutionDto?>(
                    execution);
        }

        public Task<
            PropertyMatchSalesAutomationExecutionDto?>
            CancelAsync(
                Guid executionId,
                string reason = "")
        {
            if (!_executions.TryGetValue(
                executionId,
                out var execution))
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        null);
            }

            if (execution.Status == "Completed")
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        execution);
            }

            execution.Status =
                "Cancelled";

            execution.Result =
                reason;

            execution.CancelledAt =
                DateTime.UtcNow;

            execution.UpdatedAt =
                DateTime.UtcNow;

            return Task.FromResult<
                PropertyMatchSalesAutomationExecutionDto?>(
                    execution);
        }

        public Task<
            PropertyMatchSalesAutomationExecutionDto?>
            RetryAsync(
                Guid executionId)
        {
            if (!_executions.TryGetValue(
                executionId,
                out var execution))
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        null);
            }

            if (execution.Status != "Failed")
            {
                return Task.FromResult<
                    PropertyMatchSalesAutomationExecutionDto?>(
                        execution);
            }

            execution.Status =
                execution.RequiresApproval
                    ? "Approved"
                    : "Pending";

            execution.LastError =
                string.Empty;

            execution.FailedAt =
                null;

            execution.UpdatedAt =
                DateTime.UtcNow;

            return Task.FromResult<
                PropertyMatchSalesAutomationExecutionDto?>(
                    execution);
        }

        public async Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            GetPendingAsync()
        {
            var executions =
                await GetExecutionsAsync();

            return executions
                .Where(x =>
                    x.Status == "Pending")
                .OrderByDescending(
                    x => x.ExecutionScore)
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            GetApprovedAsync()
        {
            var executions =
                await GetExecutionsAsync();

            return executions
                .Where(x =>
                    x.Status == "Approved")
                .OrderByDescending(
                    x => x.ExecutionScore)
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            GetFailedAsync()
        {
            var executions =
                await GetExecutionsAsync();

            return executions
                .Where(x =>
                    x.Status == "Failed")
                .OrderByDescending(
                    x => x.UpdatedAt)
                .ToList();
        }

        public async Task<
            PropertyMatchSalesAutomationExecutionSummaryDto>
            GetSummaryAsync()
        {
            var executions =
                await GetExecutionsAsync();

            if (executions.Count == 0)
            {
                return new
                    PropertyMatchSalesAutomationExecutionSummaryDto
                {
                    GeneratedAt =
                        DateTime.UtcNow
                };
            }

            return new
                PropertyMatchSalesAutomationExecutionSummaryDto
            {
                TotalExecutions =
                    executions.Count,

                PendingExecutions =
                    executions.Count(
                        x => x.Status == "Pending"),

                ApprovedExecutions =
                    executions.Count(
                        x => x.Status == "Approved"),

                ExecutingExecutions =
                    executions.Count(
                        x => x.Status == "Executing"),

                CompletedExecutions =
                    executions.Count(
                        x => x.Status == "Completed"),

                FailedExecutions =
                    executions.Count(
                        x => x.Status == "Failed"),

                CancelledExecutions =
                    executions.Count(
                        x => x.Status == "Cancelled"),

                ApprovalRequired =
                    executions.Count(
                        x =>
                            x.RequiresApproval &&
                            x.Status == "Pending"),

                AverageExecutionScore =
                    Math.Round(
                        executions.Average(
                            x => x.ExecutionScore),
                        2),

                TotalAttempts =
                    executions.Sum(
                        x => x.AttemptCount),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static
            PropertyMatchSalesAutomationExecutionDto
            CreateExecution(
                PropertyMatchSalesAutomationDto automation)
        {
            return new
                PropertyMatchSalesAutomationExecutionDto
            {
                ExecutionId =
                    Guid.NewGuid(),

                MatchId =
                    automation.MatchId,

                LeadId =
                    automation.LeadId,

                PropertyId =
                    automation.PropertyId,

                AutomationType =
                    automation.AutomationType,

                Action =
                    automation.Action,

                ActionTitle =
                    automation.ActionTitle,

                Channel =
                    automation.Channel,

                Status =
                    automation.RequiresApproval
                        ? "Pending"
                        : "Approved",

                ExecutionScore =
                    automation.AutomationScore,

                Urgency =
                    automation.Urgency,

                UrgencyScore =
                    automation.UrgencyScore,

                RequiresApproval =
                    automation.RequiresApproval,

                AutoExecutable =
                    automation.AutoExecutable,

                ScheduledAt =
                    automation.ExecuteAt,

                Instruction =
                    automation.Instruction,

                Reason =
                    automation.Reason,

                CreatedAt =
                    DateTime.UtcNow,

                UpdatedAt =
                    DateTime.UtcNow
            };
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

    public class PropertyMatchSalesAutomationExecutionDto
    {
        public Guid ExecutionId { get; set; }

        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string AutomationType { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string ActionTitle { get; set; }
            = string.Empty;

        public string Channel { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = "Pending";

        public decimal ExecutionScore { get; set; }

        public string Urgency { get; set; }
            = string.Empty;

        public decimal UrgencyScore { get; set; }

        public bool RequiresApproval { get; set; }

        public bool AutoExecutable { get; set; }

        public DateTime ScheduledAt { get; set; }

        public string Instruction { get; set; }
            = string.Empty;

        public string Reason { get; set; }
            = string.Empty;

        public string ApprovedBy { get; set; }
            = string.Empty;

        public string Result { get; set; }
            = string.Empty;

        public string LastError { get; set; }
            = string.Empty;

        public int AttemptCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime? FailedAt { get; set; }

        public DateTime? CancelledAt { get; set; }
    }

    public class PropertyMatchSalesAutomationExecutionSummaryDto
    {
        public int TotalExecutions { get; set; }

        public int PendingExecutions { get; set; }

        public int ApprovedExecutions { get; set; }

        public int ExecutingExecutions { get; set; }

        public int CompletedExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public int CancelledExecutions { get; set; }

        public int ApprovalRequired { get; set; }

        public decimal AverageExecutionScore { get; set; }

        public int TotalAttempts { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
