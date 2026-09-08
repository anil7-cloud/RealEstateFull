using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchOpportunityService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchOpportunityService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchOpportunityDto>>
            GetOpportunitiesAsync(
                decimal minimumScore = 60,
                int limit = 50)
        {
            minimumScore = NormalizeScore(minimumScore);
            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Where(x =>
                    x.MatchScore >= minimumScore &&
                    !IsClosed(x.Status))
                .Select(CreateOpportunity)
                .OrderByDescending(x => x.OpportunityScore)
                .ThenByDescending(x => x.MatchScore)
                .Take(limit)
                .ToList();
        }

        public async Task<List<PropertyMatchOpportunityDto>>
            GetLeadOpportunitiesAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));

            limit = NormalizeLimit(limit);

            var matches = await _matchService
                .GetByLeadIdAsync(leadId);

            return matches
                .Where(x => !IsClosed(x.Status))
                .Select(CreateOpportunity)
                .OrderByDescending(x => x.OpportunityScore)
                .Take(limit)
                .ToList();
        }

        public async Task<List<PropertyMatchOpportunityDto>>
            GetPropertyOpportunitiesAsync(
                int propertyId,
                int limit = 20)
        {
            if (propertyId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(propertyId));

            limit = NormalizeLimit(limit);

            var matches = await _matchService
                .GetByPropertyIdAsync(propertyId);

            return matches
                .Where(x => !IsClosed(x.Status))
                .Select(CreateOpportunity)
                .OrderByDescending(x => x.OpportunityScore)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchOpportunityDto?>
            GetBestOpportunityAsync()
        {
            var opportunities =
                await GetOpportunitiesAsync(0, 100);

            return opportunities
                .OrderByDescending(x => x.OpportunityScore)
                .ThenByDescending(x => x.MatchScore)
                .FirstOrDefault();
        }

        private static PropertyMatchOpportunityDto
            CreateOpportunity(
                PropertyCustomerMatchDto match)
        {
            var stageScore =
                GetStageScore(match.Status);

            var criteriaScore = 0m;

            if (match.PriceMatched)
                criteriaScore += 5;

            if (match.LocationMatched)
                criteriaScore += 5;

            if (match.PropertyTypeMatched)
                criteriaScore += 3;

            if (match.RoomCountMatched)
                criteriaScore += 3;

            if (match.SizeMatched)
                criteriaScore += 4;

            // MatchScore %70 ağırlık
            // Satış aşaması %10 ağırlık
            // Detaylı kriterler %20 ağırlık
            var opportunityScore =
                (match.MatchScore * 0.70m) +
                stageScore +
                criteriaScore;

            opportunityScore =
                Math.Clamp(
                    opportunityScore,
                    0,
                    100);

            return new PropertyMatchOpportunityDto
            {
                MatchId = match.Id,

                PropertyId = match.PropertyId,

                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                OpportunityScore =
                    Math.Round(
                        opportunityScore,
                        2),

                Stage = match.Status,

                Priority =
                    GetPriority(opportunityScore),

                RecommendedAction =
                    GetRecommendedAction(
                        opportunityScore,
                        match.Status),

                Reason = match.MatchReason,

                CreatedAt = match.CreatedAt,

                CalculatedAt = DateTime.UtcNow
            };
        }

        private static decimal GetStageScore(
            string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return 0;

            return status.Trim().ToLowerInvariant() switch
            {
                "offer" => 10,
                "meeting" => 8,
                "contacted" => 6,
                "viewed" => 4,
                "new" => 2,
                _ => 0
            };
        }

        private static string GetPriority(
            decimal score)
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

        private static string GetRecommendedAction(
            decimal score,
            string status)
        {
            if (string.Equals(
                    status,
                    "Offer",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Teklifi hemen takip et.";
            }

            if (score >= 90)
                return "Müşteriyi hemen ara ve görüşme planla.";

            if (score >= 80)
                return "Bugün müşteriyle iletişime geç.";

            if (score >= 70)
                return "İlan detaylarını gönder ve geri dönüş al.";

            if (score >= 60)
                return "Müşteri ilgisini doğrula.";

            return "Fırsatı incele.";
        }

        private static bool IsClosed(
            string status)
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

        private static decimal NormalizeScore(
            decimal score)
        {
            return Math.Clamp(score, 0, 100);
        }

        private static int NormalizeLimit(
            int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }

    public class PropertyMatchOpportunityDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public decimal OpportunityScore { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;

        public string Reason { get; set; }
            = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime CalculatedAt { get; set; }
    }
}
