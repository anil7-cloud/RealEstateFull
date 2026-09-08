namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationAnalyticsService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationExecutionService
                _executionService;

        public PropertyCustomerMatchSalesAutomationAnalyticsService(
            PropertyCustomerMatchSalesAutomationExecutionService
                executionService)
        {
            _executionService = executionService;
        }

        public async Task<PropertyMatchSalesAutomationAnalyticsDto>
            GetAnalyticsAsync()
        {
            var executions =
                await _executionService.GetExecutionsAsync();

            var dto =
                new PropertyMatchSalesAutomationAnalyticsDto
                {
                    TotalExecutions =
                        executions.Count,

                    GeneratedAt =
                        DateTime.UtcNow
                };

            if (executions.Count == 0)
                return dto;

            dto.PendingExecutions =
                CountStatus(executions, "Pending");

            dto.ApprovedExecutions =
                CountStatus(executions, "Approved");

            dto.ExecutingExecutions =
                CountStatus(executions, "Executing");

            dto.CompletedExecutions =
                CountStatus(executions, "Completed");

            dto.FailedExecutions =
                CountStatus(executions, "Failed");

            dto.CancelledExecutions =
                CountStatus(executions, "Cancelled");

            dto.SuccessRate =
                Percentage(
                    dto.CompletedExecutions,
                    dto.TotalExecutions);

            dto.FailureRate =
                Percentage(
                    dto.FailedExecutions,
                    dto.TotalExecutions);

            dto.CancellationRate =
                Percentage(
                    dto.CancelledExecutions,
                    dto.TotalExecutions);

            dto.CompletionRate =
                Percentage(
                    dto.CompletedExecutions,
                    dto.TotalExecutions);

            dto.AverageExecutionScore =
                Math.Round(
                    executions.Average(
                        x => x.ExecutionScore),
                    2);

            dto.AverageUrgencyScore =
                Math.Round(
                    executions.Average(
                        x => x.UrgencyScore),
                    2);

            dto.TotalAttempts =
                executions.Sum(
                    x => x.AttemptCount);

            dto.AverageAttempts =
                Math.Round(
                    executions.Average(
                        x => (decimal)x.AttemptCount),
                    2);

            dto.ApprovalRequiredCount =
                executions.Count(
                    x => x.RequiresApproval);

            dto.AutoExecutableCount =
                executions.Count(
                    x => x.AutoExecutable);

            dto.OverdueCount =
                executions.Count(
                    x =>
                        x.ScheduledAt < DateTime.UtcNow &&
                        !IsTerminalStatus(x.Status));

            dto.ActionPerformance =
                BuildActionPerformance(
                    executions);

            dto.ChannelPerformance =
                BuildChannelPerformance(
                    executions);

            dto.UrgencyPerformance =
                BuildUrgencyPerformance(
                    executions);

            dto.StatusDistribution =
                BuildStatusDistribution(
                    executions);

            dto.TopPerformingAction =
                dto.ActionPerformance
                    .OrderByDescending(
                        x => x.SuccessRate)
                    .ThenByDescending(
                        x => x.AverageExecutionScore)
                    .Select(x => x.Action)
                    .FirstOrDefault()
                    ?? string.Empty;

            dto.HighestFailureAction =
                dto.ActionPerformance
                    .OrderByDescending(
                        x => x.FailureRate)
                    .ThenByDescending(
                        x => x.TotalExecutions)
                    .Select(x => x.Action)
                    .FirstOrDefault()
                    ?? string.Empty;

            dto.HealthScore =
                CalculateHealthScore(dto);

            dto.HealthLevel =
                GetHealthLevel(
                    dto.HealthScore);

            dto.ManagementInsight =
                BuildManagementInsight(dto);

            return dto;
        }

        public async Task<
            List<PropertyMatchSalesAutomationActionPerformanceDto>>
            GetActionPerformanceAsync()
        {
            var executions =
                await _executionService.GetExecutionsAsync();

            return BuildActionPerformance(
                executions);
        }

        public async Task<
            List<PropertyMatchSalesAutomationChannelPerformanceDto>>
            GetChannelPerformanceAsync()
        {
            var executions =
                await _executionService.GetExecutionsAsync();

            return BuildChannelPerformance(
                executions);
        }

        public async Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            GetFailuresAsync(int limit = 20)
        {
            var executions =
                await _executionService.GetExecutionsAsync();

            return executions
                .Where(x =>
                    IsStatus(
                        x.Status,
                        "Failed"))
                .OrderByDescending(
                    x => x.ExecutionScore)
                .ThenByDescending(
                    x => x.FailedAt)
                .Take(
                    NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            GetSuccessfulExecutionsAsync(
                int limit = 20)
        {
            var executions =
                await _executionService.GetExecutionsAsync();

            return executions
                .Where(x =>
                    IsStatus(
                        x.Status,
                        "Completed"))
                .OrderByDescending(
                    x => x.ExecutionScore)
                .ThenByDescending(
                    x => x.CompletedAt)
                .Take(
                    NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationExecutionDto>>
            GetOverdueAsync(int limit = 20)
        {
            var executions =
                await _executionService.GetExecutionsAsync();

            return executions
                .Where(x =>
                    x.ScheduledAt <
                        DateTime.UtcNow &&
                    !IsTerminalStatus(
                        x.Status))
                .OrderBy(
                    x => x.ScheduledAt)
                .ThenByDescending(
                    x => x.ExecutionScore)
                .Take(
                    NormalizeLimit(limit))
                .ToList();
        }

        private static List<
            PropertyMatchSalesAutomationActionPerformanceDto>
            BuildActionPerformance(
                List<PropertyMatchSalesAutomationExecutionDto>
                    executions)
        {
            return executions
                .GroupBy(x =>
                    string.IsNullOrWhiteSpace(x.Action)
                        ? "Unknown"
                        : x.Action)
                .Select(group =>
                {
                    var total =
                        group.Count();

                    var completed =
                        group.Count(x =>
                            IsStatus(
                                x.Status,
                                "Completed"));

                    var failed =
                        group.Count(x =>
                            IsStatus(
                                x.Status,
                                "Failed"));

                    return new
                        PropertyMatchSalesAutomationActionPerformanceDto
                    {
                        Action =
                            group.Key,

                        TotalExecutions =
                            total,

                        CompletedExecutions =
                            completed,

                        FailedExecutions =
                            failed,

                        SuccessRate =
                            Percentage(
                                completed,
                                total),

                        FailureRate =
                            Percentage(
                                failed,
                                total),

                        AverageExecutionScore =
                            Math.Round(
                                group.Average(
                                    x =>
                                        x.ExecutionScore),
                                2),

                        AverageUrgencyScore =
                            Math.Round(
                                group.Average(
                                    x =>
                                        x.UrgencyScore),
                                2),

                        TotalAttempts =
                            group.Sum(
                                x => x.AttemptCount)
                    };
                })
                .OrderByDescending(
                    x => x.SuccessRate)
                .ThenByDescending(
                    x => x.TotalExecutions)
                .ToList();
        }

        private static List<
            PropertyMatchSalesAutomationChannelPerformanceDto>
            BuildChannelPerformance(
                List<PropertyMatchSalesAutomationExecutionDto>
                    executions)
        {
            return executions
                .GroupBy(x =>
                    string.IsNullOrWhiteSpace(x.Channel)
                        ? "Unknown"
                        : x.Channel)
                .Select(group =>
                {
                    var total =
                        group.Count();

                    var completed =
                        group.Count(x =>
                            IsStatus(
                                x.Status,
                                "Completed"));

                    var failed =
                        group.Count(x =>
                            IsStatus(
                                x.Status,
                                "Failed"));

                    return new
                        PropertyMatchSalesAutomationChannelPerformanceDto
                    {
                        Channel =
                            group.Key,

                        TotalExecutions =
                            total,

                        CompletedExecutions =
                            completed,

                        FailedExecutions =
                            failed,

                        SuccessRate =
                            Percentage(
                                completed,
                                total),

                        FailureRate =
                            Percentage(
                                failed,
                                total),

                        AverageExecutionScore =
                            Math.Round(
                                group.Average(
                                    x =>
                                        x.ExecutionScore),
                                2)
                    };
                })
                .OrderByDescending(
                    x => x.SuccessRate)
                .ThenByDescending(
                    x => x.TotalExecutions)
                .ToList();
        }

        private static List<
            PropertyMatchSalesAutomationUrgencyPerformanceDto>
            BuildUrgencyPerformance(
                List<PropertyMatchSalesAutomationExecutionDto>
                    executions)
        {
            return executions
                .GroupBy(x =>
                    string.IsNullOrWhiteSpace(x.Urgency)
                        ? "Unknown"
                        : x.Urgency)
                .Select(group =>
                {
                    var total =
                        group.Count();

                    var completed =
                        group.Count(x =>
                            IsStatus(
                                x.Status,
                                "Completed"));

                    var failed =
                        group.Count(x =>
                            IsStatus(
                                x.Status,
                                "Failed"));

                    return new
                        PropertyMatchSalesAutomationUrgencyPerformanceDto
                    {
                        Urgency =
                            group.Key,

                        TotalExecutions =
                            total,

                        CompletedExecutions =
                            completed,

                        FailedExecutions =
                            failed,

                        SuccessRate =
                            Percentage(
                                completed,
                                total),

                        FailureRate =
                            Percentage(
                                failed,
                                total)
                    };
                })
                .OrderByDescending(
                    x => GetUrgencyOrder(
                        x.Urgency))
                .ToList();
        }

        private static List<
            PropertyMatchSalesAutomationStatusDistributionDto>
            BuildStatusDistribution(
                List<PropertyMatchSalesAutomationExecutionDto>
                    executions)
        {
            var total =
                executions.Count;

            return executions
                .GroupBy(x =>
                    string.IsNullOrWhiteSpace(x.Status)
                        ? "Unknown"
                        : x.Status)
                .Select(group =>
                    new
                    PropertyMatchSalesAutomationStatusDistributionDto
                    {
                        Status =
                            group.Key,

                        Count =
                            group.Count(),

                        Percentage =
                            Percentage(
                                group.Count(),
                                total)
                    })
                .OrderByDescending(
                    x => x.Count)
                .ToList();
        }

        private static decimal CalculateHealthScore(
            PropertyMatchSalesAutomationAnalyticsDto dto)
        {
            var successComponent =
                dto.SuccessRate * 0.45m;

            var failureComponent =
                (100 - dto.FailureRate) *
                0.20m;

            var executionComponent =
                dto.AverageExecutionScore *
                0.20m;

            var overduePenalty =
                dto.TotalExecutions == 0
                    ? 0
                    : Percentage(
                        dto.OverdueCount,
                        dto.TotalExecutions) *
                      0.15m;

            var score =
                successComponent +
                failureComponent +
                executionComponent +
                15m -
                overduePenalty;

            return Math.Round(
                Math.Clamp(
                    score,
                    0,
                    100),
                2);
        }

        private static string GetHealthLevel(
            decimal score)
        {
            return score switch
            {
                >= 90 => "Excellent",
                >= 75 => "Strong",
                >= 60 => "Good",
                >= 45 => "NeedsAttention",
                >= 30 => "Weak",
                _ => "Critical"
            };
        }

        private static string BuildManagementInsight(
            PropertyMatchSalesAutomationAnalyticsDto dto)
        {
            if (dto.TotalExecutions == 0)
            {
                return
                    "Henüz analiz edilecek satış otomasyonu execution verisi bulunmuyor.";
            }

            if (dto.FailureRate >= 30)
            {
                return
                    $"Otomasyon başarısızlık oranı %{dto.FailureRate:N2}. " +
                    $"En fazla hata üreten aksiyon: {dto.HighestFailureAction}.";
            }

            if (dto.OverdueCount > 0)
            {
                return
                    $"{dto.OverdueCount} otomasyon zamanında çalıştırılmamış. " +
                    "Geciken satış aksiyonları önceliklendirilmelidir.";
            }

            if (dto.SuccessRate >= 80)
            {
                return
                    $"Otomasyon sistemi güçlü çalışıyor. Başarı oranı %{dto.SuccessRate:N2}. " +
                    $"En başarılı aksiyon: {dto.TopPerformingAction}.";
            }

            return
                $"Otomasyon başarı oranı %{dto.SuccessRate:N2}. " +
                $"En iyi performans gösteren aksiyon: {dto.TopPerformingAction}.";
        }

        private static int CountStatus(
            IEnumerable<PropertyMatchSalesAutomationExecutionDto>
                executions,
            string status)
        {
            return executions.Count(
                x => IsStatus(
                    x.Status,
                    status));
        }

        private static bool IsStatus(
            string? status,
            string expected)
        {
            return string.Equals(
                status,
                expected,
                StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsTerminalStatus(
            string? status)
        {
            return
                IsStatus(status, "Completed") ||
                IsStatus(status, "Failed") ||
                IsStatus(status, "Cancelled");
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

        private static int GetUrgencyOrder(
            string urgency)
        {
            return urgency switch
            {
                "Critical" => 5,
                "High" => 4,
                "Medium" => 3,
                "Low" => 2,
                "VeryLow" => 1,
                _ => 0
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

    public class PropertyMatchSalesAutomationAnalyticsDto
    {
        public int TotalExecutions { get; set; }

        public int PendingExecutions { get; set; }

        public int ApprovedExecutions { get; set; }

        public int ExecutingExecutions { get; set; }

        public int CompletedExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public int CancelledExecutions { get; set; }

        public int ApprovalRequiredCount { get; set; }

        public int AutoExecutableCount { get; set; }

        public int OverdueCount { get; set; }

        public int TotalAttempts { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public decimal CancellationRate { get; set; }

        public decimal CompletionRate { get; set; }

        public decimal AverageExecutionScore { get; set; }

        public decimal AverageUrgencyScore { get; set; }

        public decimal AverageAttempts { get; set; }

        public decimal HealthScore { get; set; }

        public string HealthLevel { get; set; }
            = string.Empty;

        public string TopPerformingAction { get; set; }
            = string.Empty;

        public string HighestFailureAction { get; set; }
            = string.Empty;

        public string ManagementInsight { get; set; }
            = string.Empty;

        public List<PropertyMatchSalesAutomationActionPerformanceDto>
            ActionPerformance { get; set; } = new();

        public List<PropertyMatchSalesAutomationChannelPerformanceDto>
            ChannelPerformance { get; set; } = new();

        public List<PropertyMatchSalesAutomationUrgencyPerformanceDto>
            UrgencyPerformance { get; set; } = new();

        public List<PropertyMatchSalesAutomationStatusDistributionDto>
            StatusDistribution { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationActionPerformanceDto
    {
        public string Action { get; set; }
            = string.Empty;

        public int TotalExecutions { get; set; }

        public int CompletedExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public int TotalAttempts { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public decimal AverageExecutionScore { get; set; }

        public decimal AverageUrgencyScore { get; set; }
    }

    public class PropertyMatchSalesAutomationChannelPerformanceDto
    {
        public string Channel { get; set; }
            = string.Empty;

        public int TotalExecutions { get; set; }

        public int CompletedExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }

        public decimal AverageExecutionScore { get; set; }
    }

    public class PropertyMatchSalesAutomationUrgencyPerformanceDto
    {
        public string Urgency { get; set; }
            = string.Empty;

        public int TotalExecutions { get; set; }

        public int CompletedExecutions { get; set; }

        public int FailedExecutions { get; set; }

        public decimal SuccessRate { get; set; }

        public decimal FailureRate { get; set; }
    }

    public class PropertyMatchSalesAutomationStatusDistributionDto
    {
        public string Status { get; set; }
            = string.Empty;

        public int Count { get; set; }

        public decimal Percentage { get; set; }
    }
}
