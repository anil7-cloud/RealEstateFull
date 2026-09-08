using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchAnalyticsService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchAnalyticsService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<PropertyMatchAnalyticsDto> GetAnalyticsAsync()
        {
            var matches = await _matchService.GetAllAsync();

            if (matches.Count == 0)
            {
                return new PropertyMatchAnalyticsDto
                {
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var total = matches.Count;

            var averageScore = matches.Average(x => x.MatchScore);

            var strongMatches = matches.Count(
                x => x.MatchScore >= 75);

            var excellentMatches = matches.Count(
                x => x.MatchScore >= 90);

            var weakMatches = matches.Count(
                x => x.MatchScore < 40);

            var uniqueLeads = matches
                .Select(x => x.LeadId)
                .Distinct()
                .Count();

            var uniqueProperties = matches
                .Select(x => x.PropertyId)
                .Distinct()
                .Count();

            var priceMatchRate =
                CalculateRate(
                    matches.Count(x => x.PriceMatched),
                    total);

            var locationMatchRate =
                CalculateRate(
                    matches.Count(x => x.LocationMatched),
                    total);

            var propertyTypeMatchRate =
                CalculateRate(
                    matches.Count(x => x.PropertyTypeMatched),
                    total);

            var roomCountMatchRate =
                CalculateRate(
                    matches.Count(x => x.RoomCountMatched),
                    total);

            var sizeMatchRate =
                CalculateRate(
                    matches.Count(x => x.SizeMatched),
                    total);

            var scoreDistribution = new PropertyMatchScoreDistributionDto
            {
                Score90To100 = matches.Count(
                    x => x.MatchScore >= 90),

                Score75To89 = matches.Count(
                    x => x.MatchScore >= 75 &&
                         x.MatchScore < 90),

                Score60To74 = matches.Count(
                    x => x.MatchScore >= 60 &&
                         x.MatchScore < 75),

                Score40To59 = matches.Count(
                    x => x.MatchScore >= 40 &&
                         x.MatchScore < 60),

                Score0To39 = matches.Count(
                    x => x.MatchScore < 40)
            };

            var topLeads = matches
                .GroupBy(x => x.LeadId)
                .Select(group => new PropertyMatchEntityPerformanceDto
                {
                    Id = group.Key,
                    MatchCount = group.Count(),
                    AverageScore = Math.Round(
                        group.Average(x => x.MatchScore),
                        2),
                    BestScore = group.Max(x => x.MatchScore)
                })
                .OrderByDescending(x => x.AverageScore)
                .ThenByDescending(x => x.BestScore)
                .Take(10)
                .ToList();

            var topProperties = matches
                .GroupBy(x => x.PropertyId)
                .Select(group => new PropertyMatchEntityPerformanceDto
                {
                    Id = group.Key,
                    MatchCount = group.Count(),
                    AverageScore = Math.Round(
                        group.Average(x => x.MatchScore),
                        2),
                    BestScore = group.Max(x => x.MatchScore)
                })
                .OrderByDescending(x => x.AverageScore)
                .ThenByDescending(x => x.BestScore)
                .Take(10)
                .ToList();

            return new PropertyMatchAnalyticsDto
            {
                TotalMatches = total,

                AverageScore =
                    Math.Round(averageScore, 2),

                StrongMatches =
                    strongMatches,

                ExcellentMatches =
                    excellentMatches,

                WeakMatches =
                    weakMatches,

                UniqueLeads =
                    uniqueLeads,

                UniqueProperties =
                    uniqueProperties,

                StrongMatchRate =
                    CalculateRate(strongMatches, total),

                ExcellentMatchRate =
                    CalculateRate(excellentMatches, total),

                PriceMatchRate =
                    priceMatchRate,

                LocationMatchRate =
                    locationMatchRate,

                PropertyTypeMatchRate =
                    propertyTypeMatchRate,

                RoomCountMatchRate =
                    roomCountMatchRate,

                SizeMatchRate =
                    sizeMatchRate,

                ScoreDistribution =
                    scoreDistribution,

                TopLeads =
                    topLeads,

                TopProperties =
                    topProperties,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal CalculateRate(
            int count,
            int total)
        {
            if (total <= 0)
                return 0;

            return Math.Round(
                (decimal)count / total * 100,
                2);
        }
    }

    public class PropertyMatchAnalyticsDto
    {
        public int TotalMatches { get; set; }

        public decimal AverageScore { get; set; }

        public int StrongMatches { get; set; }

        public int ExcellentMatches { get; set; }

        public int WeakMatches { get; set; }

        public int UniqueLeads { get; set; }

        public int UniqueProperties { get; set; }

        public decimal StrongMatchRate { get; set; }

        public decimal ExcellentMatchRate { get; set; }

        public decimal PriceMatchRate { get; set; }

        public decimal LocationMatchRate { get; set; }

        public decimal PropertyTypeMatchRate { get; set; }

        public decimal RoomCountMatchRate { get; set; }

        public decimal SizeMatchRate { get; set; }

        public PropertyMatchScoreDistributionDto ScoreDistribution
            { get; set; } = new();

        public List<PropertyMatchEntityPerformanceDto> TopLeads
            { get; set; } = new();

        public List<PropertyMatchEntityPerformanceDto> TopProperties
            { get; set; } = new();

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchScoreDistributionDto
    {
        public int Score90To100 { get; set; }

        public int Score75To89 { get; set; }

        public int Score60To74 { get; set; }

        public int Score40To59 { get; set; }

        public int Score0To39 { get; set; }
    }

    public class PropertyMatchEntityPerformanceDto
    {
        public int Id { get; set; }

        public int MatchCount { get; set; }

        public decimal AverageScore { get; set; }

        public decimal BestScore { get; set; }
    }
}
