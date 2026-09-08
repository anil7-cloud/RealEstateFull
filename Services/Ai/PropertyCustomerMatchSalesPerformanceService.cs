using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesPerformanceService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchSalesPerformanceService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<PropertyMatchSalesPerformanceDto>
            GetPerformanceAsync()
        {
            var matches = await _matchService.GetAllAsync();

            var total = matches.Count;

            var won = matches.Count(x =>
                IsStatus(x.Status, "Won"));

            var lost = matches.Count(x =>
                IsStatus(x.Status, "Lost"));

            var active = matches.Count(x =>
                !IsStatus(x.Status, "Won") &&
                !IsStatus(x.Status, "Lost"));

            var offer = matches.Count(x =>
                IsStatus(x.Status, "Offer"));

            var meeting = matches.Count(x =>
                IsStatus(x.Status, "Meeting"));

            var contacted = matches.Count(x =>
                IsStatus(x.Status, "Contacted"));

            var viewed = matches.Count(x =>
                IsStatus(x.Status, "Viewed"));

            var newLeads = matches.Count(x =>
                IsStatus(x.Status, "New"));

            var closed = won + lost;

            var conversionRate =
                closed == 0
                    ? 0
                    : (decimal)won / closed * 100m;

            var overallConversionRate =
                total == 0
                    ? 0
                    : (decimal)won / total * 100m;

            var averageMatchScore =
                total == 0
                    ? 0
                    : matches.Average(x => x.MatchScore);

            var wonMatches = matches
                .Where(x => IsStatus(x.Status, "Won"))
                .ToList();

            var averageWonMatchScore =
                wonMatches.Count == 0
                    ? 0
                    : wonMatches.Average(x => x.MatchScore);

            var highQualityMatches = matches.Count(x =>
                x.MatchScore >= 80);

            var fullCriteriaMatches = matches.Count(x =>
                x.PriceMatched &&
                x.LocationMatched &&
                x.PropertyTypeMatched &&
                x.RoomCountMatched &&
                x.SizeMatched);

            var performanceScore =
                CalculatePerformanceScore(
                    conversionRate,
                    averageMatchScore,
                    highQualityMatches,
                    total);

            return new PropertyMatchSalesPerformanceDto
            {
                TotalMatches = total,

                ActiveMatches = active,

                WonMatches = won,

                LostMatches = lost,

                NewMatches = newLeads,

                ViewedMatches = viewed,

                ContactedMatches = contacted,

                MeetingMatches = meeting,

                OfferMatches = offer,

                ConversionRate =
                    Math.Round(conversionRate, 2),

                OverallConversionRate =
                    Math.Round(overallConversionRate, 2),

                AverageMatchScore =
                    Math.Round(averageMatchScore, 2),

                AverageWonMatchScore =
                    Math.Round(averageWonMatchScore, 2),

                HighQualityMatches =
                    highQualityMatches,

                FullCriteriaMatches =
                    fullCriteriaMatches,

                PerformanceScore =
                    Math.Round(performanceScore, 2),

                PerformanceLevel =
                    GetPerformanceLevel(
                        performanceScore),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        public async Task<List<PropertyMatchStagePerformanceDto>>
            GetStagePerformanceAsync()
        {
            var matches = await _matchService.GetAllAsync();

            var stages = new[]
            {
                "New",
                "Viewed",
                "Contacted",
                "Meeting",
                "Offer",
                "Won",
                "Lost"
            };

            var total = matches.Count;

            return stages
                .Select(stage =>
                {
                    var stageMatches = matches
                        .Where(x =>
                            IsStatus(
                                x.Status,
                                stage))
                        .ToList();

                    var count = stageMatches.Count;

                    var percentage =
                        total == 0
                            ? 0
                            : (decimal)count /
                              total *
                              100m;

                    var averageScore =
                        count == 0
                            ? 0
                            : stageMatches.Average(
                                x => x.MatchScore);

                    return new PropertyMatchStagePerformanceDto
                    {
                        Stage = stage,

                        Count = count,

                        Percentage =
                            Math.Round(
                                percentage,
                                2),

                        AverageMatchScore =
                            Math.Round(
                                averageScore,
                                2)
                    };
                })
                .ToList();
        }

        public async Task<List<PropertyMatchSalesPerformanceItemDto>>
            GetTopPerformingMatchesAsync(
                int limit = 20)
        {
            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Select(CreatePerformanceItem)
                .OrderByDescending(
                    x => x.PerformanceScore)
                .ThenByDescending(
                    x => x.MatchScore)
                .Take(limit)
                .ToList();
        }

        private static PropertyMatchSalesPerformanceItemDto
            CreatePerformanceItem(
                PropertyCustomerMatchDto match)
        {
            var stageScore =
                GetStageScore(match.Status);

            var criteriaCount =
                CountMatchedCriteria(match);

            var score =
                (match.MatchScore * 0.70m) +
                stageScore +
                (criteriaCount * 3m);

            score = Math.Clamp(
                score,
                0,
                100);

            return new PropertyMatchSalesPerformanceItemDto
            {
                MatchId = match.Id,

                LeadId = match.LeadId,

                PropertyId = match.PropertyId,

                Stage = match.Status,

                MatchScore = match.MatchScore,

                MatchedCriteriaCount =
                    criteriaCount,

                PerformanceScore =
                    Math.Round(score, 2),

                PerformanceLevel =
                    GetPerformanceLevel(score)
            };
        }

        private static decimal CalculatePerformanceScore(
            decimal conversionRate,
            decimal averageMatchScore,
            int highQualityMatches,
            int total)
        {
            var qualityRate =
                total == 0
                    ? 0
                    : (decimal)highQualityMatches /
                      total *
                      100m;

            var score =
                (conversionRate * 0.45m) +
                (averageMatchScore * 0.35m) +
                (qualityRate * 0.20m);

            return Math.Clamp(
                score,
                0,
                100);
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
                "won" => 15,
                "offer" => 12,
                "meeting" => 10,
                "contacted" => 7,
                "viewed" => 4,
                "new" => 2,
                "lost" => 0,
                _ => 0
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

        private static string GetPerformanceLevel(
            decimal score)
        {
            return score switch
            {
                >= 85 => "Excellent",
                >= 70 => "VeryGood",
                >= 55 => "Good",
                >= 40 => "Average",
                >= 25 => "Weak",
                _ => "VeryWeak"
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

    public class PropertyMatchSalesPerformanceDto
    {
        public int TotalMatches { get; set; }

        public int ActiveMatches { get; set; }

        public int WonMatches { get; set; }

        public int LostMatches { get; set; }

        public int NewMatches { get; set; }

        public int ViewedMatches { get; set; }

        public int ContactedMatches { get; set; }

        public int MeetingMatches { get; set; }

        public int OfferMatches { get; set; }

        public decimal ConversionRate { get; set; }

        public decimal OverallConversionRate { get; set; }

        public decimal AverageMatchScore { get; set; }

        public decimal AverageWonMatchScore { get; set; }

        public int HighQualityMatches { get; set; }

        public int FullCriteriaMatches { get; set; }

        public decimal PerformanceScore { get; set; }

        public string PerformanceLevel { get; set; }
            = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchStagePerformanceDto
    {
        public string Stage { get; set; }
            = string.Empty;

        public int Count { get; set; }

        public decimal Percentage { get; set; }

        public decimal AverageMatchScore { get; set; }
    }

    public class PropertyMatchSalesPerformanceItemDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal MatchScore { get; set; }

        public int MatchedCriteriaCount { get; set; }

        public decimal PerformanceScore { get; set; }

        public string PerformanceLevel { get; set; }
            = string.Empty;
    }
}
