using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchRecommendationService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchRecommendationService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchRecommendationDto>>
            GetRecommendationsForLeadAsync(
                int leadId,
                decimal minimumScore = 60,
                int limit = 10)
        {
            if (leadId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));

            minimumScore = NormalizeScore(minimumScore);
            limit = NormalizeLimit(limit);

            var matches = await _matchService
                .GetBestMatchesForLeadAsync(
                    leadId,
                    100);

            return matches
                .Where(x => x.MatchScore >= minimumScore)
                .OrderByDescending(x => x.MatchScore)
                .ThenByDescending(x => x.CreatedAt)
                .Take(limit)
                .Select(CreateRecommendation)
                .ToList();
        }

        public async Task<List<PropertyMatchRecommendationDto>>
            GetRecommendedLeadsForPropertyAsync(
                int propertyId,
                decimal minimumScore = 60,
                int limit = 10)
        {
            if (propertyId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(propertyId));

            minimumScore = NormalizeScore(minimumScore);
            limit = NormalizeLimit(limit);

            var matches = await _matchService
                .GetBestLeadsForPropertyAsync(
                    propertyId,
                    100);

            return matches
                .Where(x => x.MatchScore >= minimumScore)
                .OrderByDescending(x => x.MatchScore)
                .ThenByDescending(x => x.CreatedAt)
                .Take(limit)
                .Select(CreateRecommendation)
                .ToList();
        }

        private static PropertyMatchRecommendationDto
            CreateRecommendation(
                PropertyCustomerMatchDto match)
        {
            return new PropertyMatchRecommendationDto
            {
                MatchId = match.Id,
                PropertyId = match.PropertyId,
                LeadId = match.LeadId,
                Score = match.MatchScore,
                Priority = GetPriority(match.MatchScore),
                Reason = match.MatchReason,
                Status = match.Status,
                RecommendedAt = DateTime.UtcNow
            };
        }

        private static string GetPriority(decimal score)
        {
            return score switch
            {
                >= 90 => "Critical",
                >= 80 => "VeryHigh",
                >= 70 => "High",
                >= 60 => "Medium",
                _ => "Low"
            };
        }

        private static decimal NormalizeScore(decimal score)
        {
            if (score < 0)
                return 0;

            if (score > 100)
                return 100;

            return score;
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }

    public class PropertyMatchRecommendationDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal Score { get; set; }

        public string Priority { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime RecommendedAt { get; set; }
    }
}
