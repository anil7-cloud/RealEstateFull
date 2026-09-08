namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesForecastInsightService
    {
        private readonly PropertyCustomerMatchSalesForecastService
            _forecastService;

        public PropertyCustomerMatchSalesForecastInsightService(
            PropertyCustomerMatchSalesForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        public async Task<PropertyMatchSalesForecastInsightDto>
            GetInsightAsync()
        {
            var forecastTask =
                _forecastService.GetForecastAsync();

            var itemsTask =
                _forecastService.GetForecastItemsAsync(100);

            await Task.WhenAll(
                forecastTask,
                itemsTask);

            var forecast = await forecastTask;
            var items = await itemsTask;

            var opportunities = items
                .Where(x => x.ForecastScore >= 70)
                .ToList();

            var weakOpportunities = items
                .Where(x => x.ForecastScore < 40)
                .ToList();

            var urgent = items
                .Where(x =>
                    x.ConversionProbability >= 80 ||
                    IsStatus(x.Stage, "Offer"))
                .ToList();

            var insightScore =
                CalculateInsightScore(
                    forecast,
                    items);

            return new PropertyMatchSalesForecastInsightDto
            {
                TotalOpportunities =
                    forecast.ActiveOpportunities,

                StrongOpportunities =
                    opportunities.Count,

                WeakOpportunities =
                    weakOpportunities.Count,

                UrgentOpportunities =
                    urgent.Count,

                ExpectedSales =
                    forecast.ExpectedTotalSales,

                ForecastScore =
                    forecast.ForecastScore,

                InsightScore =
                    Math.Round(
                        insightScore,
                        2),

                InsightLevel =
                    GetInsightLevel(
                        insightScore),

                MainInsight =
                    BuildMainInsight(
                        forecast),

                ManagementRecommendation =
                    BuildManagementRecommendation(
                        forecast,
                        opportunities.Count,
                        weakOpportunities.Count),

                PipelineWarning =
                    BuildPipelineWarning(
                        forecast),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<List<PropertyMatchSalesForecastInsightItemDto>>
            GetOpportunityInsightsAsync(
                int limit = 30)
        {
            var items =
                await _forecastService
                    .GetForecastItemsAsync(100);

            return items
                .Select(CreateInsightItem)
                .OrderByDescending(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesForecastInsightItemDto>>
            GetUrgentInsightsAsync(
                int limit = 20)
        {
            var items =
                await GetOpportunityInsightsAsync(100);

            return items
                .Where(x => x.IsUrgent)
                .OrderByDescending(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesForecastInsightItemDto>>
            GetWeakInsightsAsync(
                int limit = 20)
        {
            var items =
                await GetOpportunityInsightsAsync(100);

            return items
                .Where(x =>
                    x.InsightLevel == "Weak" ||
                    x.InsightLevel == "Critical")
                .OrderBy(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        private static PropertyMatchSalesForecastInsightItemDto
            CreateInsightItem(
                PropertyMatchSalesForecastItemDto item)
        {
            var stageScore =
                GetStageScore(item.Stage);

            var insightScore =
                (item.ForecastScore * 0.50m) +
                (item.ConversionProbability * 0.30m) +
                (item.Confidence * 0.10m) +
                (stageScore * 0.10m);

            insightScore =
                Math.Clamp(
                    insightScore,
                    0,
                    100);

            return new PropertyMatchSalesForecastInsightItemDto
            {
                MatchId =
                    item.MatchId,

                LeadId =
                    item.LeadId,

                PropertyId =
                    item.PropertyId,

                Stage =
                    item.Stage,

                ConversionProbability =
                    item.ConversionProbability,

                ForecastScore =
                    item.ForecastScore,

                InsightScore =
                    Math.Round(
                        insightScore,
                        2),

                InsightLevel =
                    GetInsightLevel(
                        insightScore),

                IsUrgent =
                    item.ConversionProbability >= 80 ||
                    IsStatus(
                        item.Stage,
                        "Offer"),

                Insight =
                    BuildOpportunityInsight(
                        item),

                RecommendedAction =
                    item.RecommendedAction
            };
        }

        private static decimal CalculateInsightScore(
            PropertyMatchSalesForecastDto forecast,
            List<PropertyMatchSalesForecastItemDto> items)
        {
            if (items.Count == 0)
                return 0;

            var strongRate =
                (decimal)items.Count(
                    x => x.ForecastScore >= 70)
                /
                items.Count *
                100m;

            var advancedStageRate =
                (decimal)items.Count(
                    x =>
                        IsStatus(x.Stage, "Meeting") ||
                        IsStatus(x.Stage, "Offer"))
                /
                items.Count *
                100m;

            var score =
                (forecast.ForecastScore * 0.55m) +
                (strongRate * 0.25m) +
                (advancedStageRate * 0.20m);

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static string BuildMainInsight(
            PropertyMatchSalesForecastDto forecast)
        {
            if (forecast.ActiveOpportunities == 0)
            {
                return
                    "Aktif satış fırsatı bulunmuyor.";
            }

            if (forecast.ForecastScore >= 80)
            {
                return
                    "Pipeline güçlü ve beklenen satış performansı yüksek.";
            }

            if (forecast.ForecastScore >= 65)
            {
                return
                    "Satış pipeline'ı sağlıklı ve güçlü fırsatlar bulunuyor.";
            }

            if (forecast.ForecastScore >= 50)
            {
                return
                    "Pipeline orta seviyede; güçlü fırsatların takibi önemli.";
            }

            if (forecast.ForecastScore >= 35)
            {
                return
                    "Pipeline zayıflıyor ve satış takibinin artırılması gerekiyor.";
            }

            return
                "Satış pipeline'ı kritik seviyede.";
        }

        private static string BuildManagementRecommendation(
            PropertyMatchSalesForecastDto forecast,
            int strongCount,
            int weakCount)
        {
            if (forecast.ActiveOpportunities == 0)
            {
                return
                    "Yeni müşteri ve lead üretimine odaklan.";
            }

            if (strongCount >= weakCount * 2 &&
                strongCount > 0)
            {
                return
                    "Güçlü fırsatlara satış ekibi kapasitesi ayır ve kapanışı hızlandır.";
            }

            if (weakCount > strongCount)
            {
                return
                    "Zayıf fırsatları yeniden nitelendir ve düşük kaliteli pipeline'ı temizle.";
            }

            if (forecast.OfferStage > 0)
            {
                return
                    "Teklif aşamasındaki müşterileri önceliklendir.";
            }

            if (forecast.MeetingStage > 0)
            {
                return
                    "Görüşme aşamasındaki müşterileri teklif aşamasına taşı.";
            }

            return
                "Yüksek dönüşüm olasılıklı müşterilere öncelik ver.";
        }

        private static string BuildPipelineWarning(
            PropertyMatchSalesForecastDto forecast)
        {
            if (forecast.ActiveOpportunities == 0)
            {
                return
                    "UYARI: Aktif satış pipeline'ı boş.";
            }

            if (forecast.PipelineHealth == "Critical")
            {
                return
                    "UYARI: Pipeline sağlığı kritik seviyede.";
            }

            if (forecast.PipelineHealth == "Weak")
            {
                return
                    "UYARI: Pipeline içinde güçlü fırsat oranı düşük.";
            }

            if (forecast.HighProbabilityOpportunities == 0)
            {
                return
                    "UYARI: Yüksek olasılıklı satış fırsatı bulunmuyor.";
            }

            return
                "Kritik pipeline uyarısı bulunmuyor.";
        }

        private static string BuildOpportunityInsight(
            PropertyMatchSalesForecastItemDto item)
        {
            if (IsStatus(item.Stage, "Offer"))
            {
                return
                    "Fırsat teklif aşamasında; satış kapanışına çok yakın.";
            }

            if (item.ConversionProbability >= 85)
            {
                return
                    "Çok yüksek dönüşüm olasılığına sahip satış fırsatı.";
            }

            if (item.ConversionProbability >= 70)
            {
                return
                    "Güçlü satış fırsatı; hızlı takip öneriliyor.";
            }

            if (item.ConversionProbability >= 50)
            {
                return
                    "Orta seviyeli fırsat; müşteri ilgisi güçlendirilmeli.";
            }

            return
                "Düşük dönüşüm olasılığı; müşteri ihtiyacı yeniden analiz edilmeli.";
        }

        private static decimal GetStageScore(
            string stage)
        {
            if (string.IsNullOrWhiteSpace(stage))
                return 20;

            return stage
                .Trim()
                .ToLowerInvariant() switch
            {
                "offer" => 100,
                "meeting" => 85,
                "contacted" => 70,
                "viewed" => 50,
                "new" => 35,
                _ => 20
            };
        }

        private static string GetInsightLevel(
            decimal score)
        {
            return score switch
            {
                >= 85 => "Excellent",
                >= 70 => "Strong",
                >= 55 => "Good",
                >= 40 => "Weak",
                _ => "Critical"
            };
        }

        private static bool IsStatus(
            string status,
            string expected)
        {
            return string.Equals(
                status,
                expected,
                StringComparison.OrdinalIgnoreCase);
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

    public class PropertyMatchSalesForecastInsightDto
    {
        public int TotalOpportunities { get; set; }

        public int StrongOpportunities { get; set; }

        public int WeakOpportunities { get; set; }

        public int UrgentOpportunities { get; set; }

        public decimal ExpectedSales { get; set; }

        public decimal ForecastScore { get; set; }

        public decimal InsightScore { get; set; }

        public string InsightLevel { get; set; }
            = string.Empty;

        public string MainInsight { get; set; }
            = string.Empty;

        public string ManagementRecommendation { get; set; }
            = string.Empty;

        public string PipelineWarning { get; set; }
            = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesForecastInsightItemDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal ConversionProbability { get; set; }

        public decimal ForecastScore { get; set; }

        public decimal InsightScore { get; set; }

        public string InsightLevel { get; set; }
            = string.Empty;

        public bool IsUrgent { get; set; }

        public string Insight { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;
    }
}
