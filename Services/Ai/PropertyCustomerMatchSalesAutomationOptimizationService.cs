namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationOptimizationService
    {
        private readonly
            PropertyCustomerMatchSalesAutomationAnalyticsService
                _analyticsService;

        public PropertyCustomerMatchSalesAutomationOptimizationService(
            PropertyCustomerMatchSalesAutomationAnalyticsService
                analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public async Task<PropertyMatchSalesAutomationOptimizationDto>
            GetOptimizationAsync()
        {
            var analyticsTask =
                _analyticsService.GetAnalyticsAsync();

            var actionsTask =
                _analyticsService.GetActionPerformanceAsync();

            var channelsTask =
                _analyticsService.GetChannelPerformanceAsync();

            await Task.WhenAll(
                analyticsTask,
                actionsTask,
                channelsTask);

            var analytics =
                await analyticsTask;

            var actions =
                await actionsTask;

            var channels =
                await channelsTask;

            var recommendations =
                BuildRecommendations(
                    analytics,
                    actions,
                    channels);

            var score =
                CalculateOptimizationScore(
                    analytics,
                    recommendations);

            return new PropertyMatchSalesAutomationOptimizationDto
            {
                CurrentHealthScore =
                    analytics.HealthScore,

                CurrentSuccessRate =
                    analytics.SuccessRate,

                CurrentFailureRate =
                    analytics.FailureRate,

                OverdueCount =
                    analytics.OverdueCount,

                OptimizationScore =
                    Math.Round(score, 2),

                OptimizationLevel =
                    GetOptimizationLevel(score),

                RecommendedChanges =
                    recommendations.Count,

                HighPriorityChanges =
                    recommendations.Count(
                        x => x.Priority == "High" ||
                             x.Priority == "Critical"),

                Recommendations =
                    recommendations,

                ExecutiveRecommendation =
                    BuildExecutiveRecommendation(
                        analytics,
                        recommendations),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<
            List<PropertyMatchSalesAutomationOptimizationItemDto>>
            GetRecommendationsAsync(
                int limit = 30)
        {
            var optimization =
                await GetOptimizationAsync();

            return optimization.Recommendations
                .OrderByDescending(
                    x => x.OptimizationImpact)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationOptimizationItemDto>>
            GetCriticalRecommendationsAsync(
                int limit = 20)
        {
            var recommendations =
                await GetRecommendationsAsync(100);

            return recommendations
                .Where(x =>
                    x.Priority == "Critical")
                .OrderByDescending(
                    x => x.OptimizationImpact)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationOptimizationItemDto>>
            GetChannelRecommendationsAsync(
                int limit = 20)
        {
            var recommendations =
                await GetRecommendationsAsync(100);

            return recommendations
                .Where(x =>
                    x.OptimizationType ==
                    "Channel")
                .OrderByDescending(
                    x => x.OptimizationImpact)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<
            List<PropertyMatchSalesAutomationOptimizationItemDto>>
            GetTimingRecommendationsAsync(
                int limit = 20)
        {
            var recommendations =
                await GetRecommendationsAsync(100);

            return recommendations
                .Where(x =>
                    x.OptimizationType ==
                    "Timing")
                .OrderByDescending(
                    x => x.OptimizationImpact)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        private static List<
            PropertyMatchSalesAutomationOptimizationItemDto>
            BuildRecommendations(
                PropertyMatchSalesAutomationAnalyticsDto analytics,
                List<PropertyMatchSalesAutomationActionPerformanceDto>
                    actions,
                List<PropertyMatchSalesAutomationChannelPerformanceDto>
                    channels)
        {
            var result =
                new List<
                    PropertyMatchSalesAutomationOptimizationItemDto>();

            foreach (var action in actions)
            {
                if (action.FailureRate >= 40)
                {
                    result.Add(new
                        PropertyMatchSalesAutomationOptimizationItemDto
                    {
                        OptimizationType =
                            "Action",

                        Target =
                            action.Action,

                        Priority =
                            "Critical",

                        CurrentMetric =
                            action.FailureRate,

                        TargetMetric =
                            20,

                        OptimizationImpact =
                            CalculateImpact(
                                action.FailureRate,
                                20),

                        Recommendation =
                            $"'{action.Action}' aksiyonunun başarısızlık oranı yüksek. " +
                            "Aksiyon kuralları ve müşteri segmentasyonu yeniden düzenlenmeli."
                    });

                    continue;
                }

                if (action.FailureRate >= 25)
                {
                    result.Add(new
                        PropertyMatchSalesAutomationOptimizationItemDto
                    {
                        OptimizationType =
                            "Action",

                        Target =
                            action.Action,

                        Priority =
                            "High",

                        CurrentMetric =
                            action.FailureRate,

                        TargetMetric =
                            15,

                        OptimizationImpact =
                            CalculateImpact(
                                action.FailureRate,
                                15),

                        Recommendation =
                            $"'{action.Action}' aksiyonunda başarısızlık oranını azaltmak için " +
                            "tetikleme kriterleri optimize edilmeli."
                    });
                }

                if (action.SuccessRate < 50 &&
                    action.TotalExecutions >= 3)
                {
                    result.Add(new
                        PropertyMatchSalesAutomationOptimizationItemDto
                    {
                        OptimizationType =
                            "Priority",

                        Target =
                            action.Action,

                        Priority =
                            "High",

                        CurrentMetric =
                            action.SuccessRate,

                        TargetMetric =
                            70,

                        OptimizationImpact =
                            CalculateImpact(
                                70,
                                action.SuccessRate),

                        Recommendation =
                            $"'{action.Action}' aksiyonunun öncelik skoru yeniden kalibre edilmeli."
                    });
                }
            }

            foreach (var channel in channels)
            {
                if (channel.TotalExecutions < 2)
                    continue;

                if (channel.SuccessRate < 40)
                {
                    var bestChannel =
                        channels
                            .Where(x =>
                                x.Channel != channel.Channel)
                            .OrderByDescending(
                                x => x.SuccessRate)
                            .FirstOrDefault();

                    result.Add(new
                        PropertyMatchSalesAutomationOptimizationItemDto
                    {
                        OptimizationType =
                            "Channel",

                        Target =
                            channel.Channel,

                        Priority =
                            channel.FailureRate >= 50
                                ? "Critical"
                                : "High",

                        CurrentMetric =
                            channel.SuccessRate,

                        TargetMetric =
                            bestChannel?.SuccessRate ?? 70,

                        OptimizationImpact =
                            CalculateImpact(
                                bestChannel?.SuccessRate ?? 70,
                                channel.SuccessRate),

                        Recommendation =
                            bestChannel == null
                                ? $"{channel.Channel} kanalının satış performansı düşük. Alternatif iletişim kanalları test edilmeli."
                                : $"{channel.Channel} yerine uygun müşterilerde {bestChannel.Channel} kanalı test edilmeli."
                    });
                }
            }

            if (analytics.OverdueCount > 0)
            {
                var overdueRate =
                    analytics.TotalExecutions == 0
                        ? 0
                        : Math.Round(
                            ((decimal)analytics.OverdueCount /
                             analytics.TotalExecutions) *
                            100m,
                            2);

                result.Add(new
                    PropertyMatchSalesAutomationOptimizationItemDto
                {
                    OptimizationType =
                        "Timing",

                    Target =
                        "ExecutionSchedule",

                    Priority =
                        overdueRate >= 25
                            ? "Critical"
                            : "High",

                    CurrentMetric =
                        overdueRate,

                    TargetMetric =
                        5,

                    OptimizationImpact =
                        CalculateImpact(
                            overdueRate,
                            5),

                    Recommendation =
                        "Geciken otomasyonlar için execution süreleri kısaltılmalı ve yüksek öncelikli görevler daha erken planlanmalı."
                });
            }

            if (analytics.AverageAttempts > 2)
            {
                result.Add(new
                    PropertyMatchSalesAutomationOptimizationItemDto
                {
                    OptimizationType =
                        "Retry",

                    Target =
                        "ExecutionRetry",

                    Priority =
                        analytics.AverageAttempts >= 4
                            ? "Critical"
                            : "Medium",

                    CurrentMetric =
                        analytics.AverageAttempts,

                    TargetMetric =
                        1.5m,

                    OptimizationImpact =
                        Math.Clamp(
                            analytics.AverageAttempts * 15m,
                            0,
                            100),

                    Recommendation =
                        "Tekrarlanan execution denemeleri azaltılmalı; hata nedenleri aksiyon ve kanal bazında analiz edilmeli."
                });
            }

            if (analytics.SuccessRate < 60 &&
                analytics.TotalExecutions > 0)
            {
                result.Add(new
                    PropertyMatchSalesAutomationOptimizationItemDto
                {
                    OptimizationType =
                        "Global",

                    Target =
                        "AutomationEngine",

                    Priority =
                        analytics.SuccessRate < 40
                            ? "Critical"
                            : "High",

                    CurrentMetric =
                        analytics.SuccessRate,

                    TargetMetric =
                        75,

                    OptimizationImpact =
                        CalculateImpact(
                            75,
                            analytics.SuccessRate),

                    Recommendation =
                        "Genel otomasyon başarı oranı düşük. Aksiyon seçimi, kanal ve zamanlama modelleri birlikte yeniden optimize edilmeli."
                });
            }

            return result
                .OrderByDescending(
                    x => PriorityOrder(x.Priority))
                .ThenByDescending(
                    x => x.OptimizationImpact)
                .ToList();
        }

        private static decimal CalculateOptimizationScore(
            PropertyMatchSalesAutomationAnalyticsDto analytics,
            List<PropertyMatchSalesAutomationOptimizationItemDto>
                recommendations)
        {
            if (analytics.TotalExecutions == 0)
                return 0;

            var healthComponent =
                analytics.HealthScore * 0.50m;

            var successComponent =
                analytics.SuccessRate * 0.30m;

            var failureComponent =
                (100m - analytics.FailureRate) *
                0.20m;

            var penalty =
                recommendations.Sum(
                    x => x.Priority switch
                    {
                        "Critical" => 4m,
                        "High" => 2m,
                        "Medium" => 1m,
                        _ => 0.5m
                    });

            return Math.Clamp(
                healthComponent +
                successComponent +
                failureComponent -
                penalty,
                0,
                100);
        }

        private static decimal CalculateImpact(
            decimal first,
            decimal second)
        {
            return Math.Round(
                Math.Clamp(
                    Math.Abs(first - second),
                    0,
                    100),
                2);
        }

        private static string GetOptimizationLevel(
            decimal score)
        {
            return score switch
            {
                >= 85 => "Optimized",
                >= 70 => "Strong",
                >= 55 => "Moderate",
                >= 40 => "NeedsOptimization",
                _ => "Critical"
            };
        }

        private static string BuildExecutiveRecommendation(
            PropertyMatchSalesAutomationAnalyticsDto analytics,
            List<PropertyMatchSalesAutomationOptimizationItemDto>
                recommendations)
        {
            if (analytics.TotalExecutions == 0)
            {
                return
                    "Optimizasyon için henüz yeterli execution verisi bulunmuyor.";
            }

            var critical =
                recommendations.Count(
                    x => x.Priority == "Critical");

            var high =
                recommendations.Count(
                    x => x.Priority == "High");

            if (critical > 0)
            {
                return
                    $"{critical} kritik optimizasyon alanı tespit edildi. " +
                    "Kritik aksiyonlar uygulanmadan otomasyon hacminin artırılması önerilmez.";
            }

            if (high > 0)
            {
                return
                    $"{high} yüksek öncelikli optimizasyon alanı bulunuyor. " +
                    "Kanal, aksiyon ve zamanlama ayarları iyileştirilmeli.";
            }

            if (analytics.SuccessRate >= 80)
            {
                return
                    "Satış otomasyonu güçlü performans gösteriyor. Mevcut yapı korunarak kontrollü ölçekleme yapılabilir.";
            }

            return
                "Sistem çalışıyor ancak satış otomasyonu performansında ek optimizasyon fırsatları bulunuyor.";
        }

        private static int PriorityOrder(
            string priority)
        {
            return priority switch
            {
                "Critical" => 4,
                "High" => 3,
                "Medium" => 2,
                "Low" => 1,
                _ => 0
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

    public class PropertyMatchSalesAutomationOptimizationDto
    {
        public decimal CurrentHealthScore { get; set; }

        public decimal CurrentSuccessRate { get; set; }

        public decimal CurrentFailureRate { get; set; }

        public int OverdueCount { get; set; }

        public decimal OptimizationScore { get; set; }

        public string OptimizationLevel { get; set; }
            = string.Empty;

        public int RecommendedChanges { get; set; }

        public int HighPriorityChanges { get; set; }

        public string ExecutiveRecommendation { get; set; }
            = string.Empty;

        public List<PropertyMatchSalesAutomationOptimizationItemDto>
            Recommendations { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationOptimizationItemDto
    {
        public string OptimizationType { get; set; }
            = string.Empty;

        public string Target { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public decimal CurrentMetric { get; set; }

        public decimal TargetMetric { get; set; }

        public decimal OptimizationImpact { get; set; }

        public string Recommendation { get; set; }
            = string.Empty;
    }
}
