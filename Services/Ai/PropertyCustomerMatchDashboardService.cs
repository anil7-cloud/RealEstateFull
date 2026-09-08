using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchDashboardService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchDashboardService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<PropertyCustomerMatchDashboardDto>
            GetDashboardAsync()
        {
            var matches = await _matchService.GetAllAsync();

            var totalMatches = matches.Count;

            var averageScore = totalMatches == 0
                ? 0
                : matches.Average(x => x.MatchScore);

            var excellentMatches = matches.Count(
                x => x.MatchScore >= 90);

            var highPotentialMatches = matches.Count(
                x => x.MatchScore >= 75);

            var mediumMatches = matches.Count(
                x => x.MatchScore >= 60 &&
                     x.MatchScore < 75);

            var lowMatches = matches.Count(
                x => x.MatchScore < 60);

            var hotLeads = matches
                .Where(x => x.MatchScore >= 90)
                .Select(x => x.LeadId)
                .Distinct()
                .Count();

            var matchedProperties = matches
                .Select(x => x.PropertyId)
                .Distinct()
                .Count();

            var matchedLeads = matches
                .Select(x => x.LeadId)
                .Distinct()
                .Count();

            var bestMatch = matches
                .OrderByDescending(x => x.MatchScore)
                .FirstOrDefault();

            return new PropertyCustomerMatchDashboardDto
            {
                TotalMatches = totalMatches,

                AverageMatchScore =
                    Math.Round(averageScore, 2),

                ExcellentMatches =
                    excellentMatches,

                HighPotentialMatches =
                    highPotentialMatches,

                MediumMatches =
                    mediumMatches,

                LowMatches =
                    lowMatches,

                HotLeads =
                    hotLeads,

                MatchedProperties =
                    matchedProperties,

                MatchedLeads =
                    matchedLeads,

                BestMatchScore =
                    bestMatch?.MatchScore ?? 0,

                BestMatchPropertyId =
                    bestMatch?.PropertyId,

                BestMatchLeadId =
                    bestMatch?.LeadId,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }
    }

    public class PropertyCustomerMatchDashboardDto
    {
        public int TotalMatches { get; set; }

        public decimal AverageMatchScore { get; set; }

        public int ExcellentMatches { get; set; }

        public int HighPotentialMatches { get; set; }

        public int MediumMatches { get; set; }

        public int LowMatches { get; set; }

        public int HotLeads { get; set; }

        public int MatchedProperties { get; set; }

        public int MatchedLeads { get; set; }

        public decimal BestMatchScore { get; set; }

        public int? BestMatchPropertyId { get; set; }

        public int? BestMatchLeadId { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
