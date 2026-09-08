using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesInsightService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchSalesInsightService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchSalesInsightDto>>
            GetInsightsAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Where(x => !IsClosed(x.Status))
                .Select(CreateInsight)
                .OrderByDescending(x => x.InsightScore)
                .ThenByDescending(x => x.MatchScore)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchSalesInsightDto?>
            GetMatchInsightAsync(int matchId)
        {
            if (matchId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));

            var match = await _matchService.GetByIdAsync(matchId);

            if (match == null)
                return null;

            return CreateInsight(match);
        }

        public async Task<List<PropertyMatchSalesInsightDto>>
            GetLeadInsightsAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));

            var matches = await _matchService
                .GetByLeadIdAsync(leadId);

            return matches
                .Where(x => !IsClosed(x.Status))
                .Select(CreateInsight)
                .OrderByDescending(x => x.InsightScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchSalesInsightSummaryDto>
            GetSummaryAsync()
        {
            var matches = await _matchService.GetAllAsync();

            var active = matches
                .Where(x => !IsClosed(x.Status))
                .ToList();

            if (active.Count == 0)
            {
                return new PropertyMatchSalesInsightSummaryDto
                {
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var insights = active
                .Select(CreateInsight)
                .ToList();

            return new PropertyMatchSalesInsightSummaryDto
            {
                TotalActiveMatches = active.Count,

                HotOpportunities =
                    insights.Count(x =>
                        x.InsightLevel == "Hot"),

                StrongOpportunities =
                    insights.Count(x =>
                        x.InsightLevel == "Strong"),

                MediumOpportunities =
                    insights.Count(x =>
                        x.InsightLevel == "Medium"),

                WeakOpportunities =
                    insights.Count(x =>
                        x.InsightLevel == "Weak"),

                AverageMatchScore =
                    Math.Round(
                        active.Average(x => x.MatchScore),
                        2),

                AverageInsightScore =
                    Math.Round(
                        insights.Average(x => x.InsightScore),
                        2),

                FullCriteriaMatches =
                    active.Count(x =>
                        x.PriceMatched &&
                        x.LocationMatched &&
                        x.PropertyTypeMatched &&
                        x.RoomCountMatched &&
                        x.SizeMatched),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static PropertyMatchSalesInsightDto
            CreateInsight(PropertyCustomerMatchDto match)
        {
            var criteriaCount = CountMatchedCriteria(match);

            var criteriaScore =
                criteriaCount * 5m;

            var stageScore =
                GetStageScore(match.Status);

            var insightScore =
                (match.MatchScore * 0.65m) +
                criteriaScore +
                stageScore;

            insightScore = Math.Clamp(
                insightScore,
                0,
                100);

            return new PropertyMatchSalesInsightDto
            {
                MatchId = match.Id,

                PropertyId = match.PropertyId,

                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                InsightScore =
                    Math.Round(insightScore, 2),

                InsightLevel =
                    GetInsightLevel(insightScore),

                Stage = match.Status,

                MatchedCriteriaCount =
                    criteriaCount,

                MainInsight =
                    BuildMainInsight(
                        match,
                        insightScore,
                        criteriaCount),

                Recommendation =
                    BuildRecommendation(
                        match,
                        insightScore),

                Risk =
                    BuildRisk(match),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static int CountMatchedCriteria(
            PropertyCustomerMatchDto match)
        {
            var count = 0;

            if (match.PriceMatched)
                count++;

            if (match.LocationMatched)
                count++;

            if (match.PropertyTypeMatched)
                count++;

            if (match.RoomCountMatched)
                count++;

            if (match.SizeMatched)
                count++;

            return count;
        }

        private static decimal GetStageScore(
            string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return 0;

            return status
                .Trim()
                .ToLowerInvariant() switch
            {
                "offer" => 10,
                "meeting" => 8,
                "contacted" => 6,
                "viewed" => 4,
                "new" => 2,
                _ => 0
            };
        }

        private static string GetInsightLevel(
            decimal score)
        {
            return score switch
            {
                >= 85 => "Hot",
                >= 70 => "Strong",
                >= 50 => "Medium",
                _ => "Weak"
            };
        }

        private static string BuildMainInsight(
            PropertyCustomerMatchDto match,
            decimal score,
            int criteriaCount)
        {
            if (score >= 85)
            {
                return
                    $"Lead #{match.LeadId} ile Property " +
                    $"#{match.PropertyId} güçlü satış potansiyeline sahip.";
            }

            if (criteriaCount >= 4)
            {
                return
                    "Müşteri tercihleri ilanın büyük bölümüyle eşleşiyor.";
            }

            if (match.MatchScore >= 70)
            {
                return
                    "Genel eşleşme skoru yüksek ancak bazı kriterler eksik.";
            }

            return
                "Eşleşme mevcut fakat satış öncesi müşteri ihtiyacı " +
                "daha ayrıntılı doğrulanmalı.";
        }

        private static string BuildRecommendation(
            PropertyCustomerMatchDto match,
            decimal score)
        {
            if (string.Equals(
                    match.Status,
                    "Offer",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Teklifi takip et ve kapanış görüşmesi yap.";
            }

            if (string.Equals(
                    match.Status,
                    "Meeting",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Görüşme sonucunu takip et ve teklif hazırla.";
            }

            if (score >= 85)
                return "Müşteriyi hemen ara ve görüşme planla.";

            if (score >= 70)
                return "İlanı müşteriye gönder ve geri dönüş al.";

            if (score >= 50)
                return "Müşteri ihtiyaçlarını tekrar doğrula.";

            return "Alternatif ilanları değerlendir.";
        }

        private static string BuildRisk(
            PropertyCustomerMatchDto match)
        {
            var risks = new List<string>();

            if (!match.PriceMatched)
                risks.Add("Fiyat uyumsuzluğu");

            if (!match.LocationMatched)
                risks.Add("Lokasyon uyumsuzluğu");

            if (!match.PropertyTypeMatched)
                risks.Add("İlan türü uyumsuzluğu");

            if (!match.RoomCountMatched)
                risks.Add("Oda sayısı uyumsuzluğu");

            if (!match.SizeMatched)
                risks.Add("Metrekare uyumsuzluğu");

            return risks.Count == 0
                ? "Belirgin risk bulunamadı."
                : string.Join(", ", risks);
        }

        private static bool IsClosed(string status)
        {
            return string.Equals(
                       status,
                       "Won",
                       StringComparison.OrdinalIgnoreCase)
                   ||
                   string.Equals(
                       status,
                       "Lost",
                       StringComparison.OrdinalIgnoreCase);
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }

    public class PropertyMatchSalesInsightDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public decimal InsightScore { get; set; }

        public string InsightLevel { get; set; } = string.Empty;

        public string Stage { get; set; } = string.Empty;

        public int MatchedCriteriaCount { get; set; }

        public string MainInsight { get; set; } = string.Empty;

        public string Recommendation { get; set; } = string.Empty;

        public string Risk { get; set; } = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesInsightSummaryDto
    {
        public int TotalActiveMatches { get; set; }

        public int HotOpportunities { get; set; }

        public int StrongOpportunities { get; set; }

        public int MediumOpportunities { get; set; }

        public int WeakOpportunities { get; set; }

        public decimal AverageMatchScore { get; set; }

        public decimal AverageInsightScore { get; set; }

        public int FullCriteriaMatches { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
