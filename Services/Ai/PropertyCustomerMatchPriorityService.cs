using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchPriorityService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchPriorityService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchPriorityDto>>
            GetPrioritiesAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Where(x => !IsClosed(x.Status))
                .Select(CreatePriority)
                .OrderByDescending(x => x.PriorityScore)
                .ThenByDescending(x => x.MatchScore)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchPriorityDto?>
            GetMatchPriorityAsync(int matchId)
        {
            if (matchId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));

            var match = await _matchService
                .GetByIdAsync(matchId);

            if (match == null)
                return null;

            return CreatePriority(match);
        }

        public async Task<List<PropertyMatchPriorityDto>>
            GetLeadPrioritiesAsync(
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
                .Select(CreatePriority)
                .OrderByDescending(x => x.PriorityScore)
                .Take(limit)
                .ToList();
        }

        public async Task<List<PropertyMatchPriorityDto>>
            GetPropertyPrioritiesAsync(
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
                .Select(CreatePriority)
                .OrderByDescending(x => x.PriorityScore)
                .Take(limit)
                .ToList();
        }

        private static PropertyMatchPriorityDto CreatePriority(
            PropertyCustomerMatchDto match)
        {
            var stageScore = GetStageScore(match.Status);
            var criteriaScore = GetCriteriaScore(match);
            var urgencyScore = GetUrgencyScore(match);

            var priorityScore =
                (match.MatchScore * 0.55m) +
                stageScore +
                criteriaScore +
                urgencyScore;

            priorityScore = Math.Clamp(
                priorityScore,
                0,
                100);

            return new PropertyMatchPriorityDto
            {
                MatchId = match.Id,
                PropertyId = match.PropertyId,
                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                PriorityScore = Math.Round(
                    priorityScore,
                    2),

                PriorityLevel =
                    GetPriorityLevel(priorityScore),

                CurrentStage =
                    match.Status,

                RecommendedAction =
                    GetRecommendedAction(
                        priorityScore,
                        match.Status),

                Reason =
                    BuildReason(
                        match,
                        stageScore,
                        criteriaScore,
                        urgencyScore),

                CalculatedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal GetStageScore(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return 0;

            return status
                .Trim()
                .ToLowerInvariant() switch
            {
                "offer" => 20,
                "meeting" => 16,
                "contacted" => 12,
                "viewed" => 8,
                "new" => 5,
                _ => 0
            };
        }

        private static decimal GetCriteriaScore(
            PropertyCustomerMatchDto match)
        {
            decimal score = 0;

            if (match.PriceMatched)
                score += 5;

            if (match.LocationMatched)
                score += 5;

            if (match.PropertyTypeMatched)
                score += 3;

            if (match.RoomCountMatched)
                score += 3;

            if (match.SizeMatched)
                score += 4;

            return score;
        }

        private static decimal GetUrgencyScore(
            PropertyCustomerMatchDto match)
        {
            var referenceDate =
                match.UpdatedAt ??
                match.CreatedAt;

            var age =
                DateTime.UtcNow - referenceDate;

            if (age.TotalDays >= 7)
                return 5;

            if (age.TotalDays >= 3)
                return 4;

            if (age.TotalDays >= 1)
                return 3;

            if (age.TotalHours >= 12)
                return 2;

            return 1;
        }

        private static string GetPriorityLevel(
            decimal score)
        {
            return score switch
            {
                >= 90 => "Critical",
                >= 80 => "VeryHigh",
                >= 70 => "High",
                >= 55 => "Medium",
                >= 40 => "Low",
                _ => "VeryLow"
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

            if (string.Equals(
                    status,
                    "Meeting",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Görüşme sonrası müşteriyi ara.";
            }

            if (score >= 90)
                return "Müşteriye hemen ulaş.";

            if (score >= 80)
                return "Bugün telefon görüşmesi yap.";

            if (score >= 70)
                return "İlan detaylarını gönder ve görüşme planla.";

            if (score >= 55)
                return "Müşteri ilgisini doğrula.";

            return "Eşleşmeyi incele.";
        }

        private static string BuildReason(
            PropertyCustomerMatchDto match,
            decimal stageScore,
            decimal criteriaScore,
            decimal urgencyScore)
        {
            return
                $"Match: {match.MatchScore:N0}, " +
                $"Stage: {stageScore:N0}, " +
                $"Criteria: {criteriaScore:N0}, " +
                $"Urgency: {urgencyScore:N0}";
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

    public class PropertyMatchPriorityDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public decimal PriorityScore { get; set; }

        public string PriorityLevel { get; set; }
            = string.Empty;

        public string CurrentStage { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;

        public string Reason { get; set; }
            = string.Empty;

        public DateTime CalculatedAt { get; set; }
    }
}
