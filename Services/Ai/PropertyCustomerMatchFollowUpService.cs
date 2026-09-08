using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchFollowUpService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchFollowUpService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchFollowUpDto>>
            GetFollowUpsAsync(
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
                .OrderByDescending(x => x.MatchScore)
                .ThenBy(x => GetFollowUpDate(x))
                .Take(limit)
                .Select(CreateFollowUp)
                .ToList();
        }

        public async Task<List<PropertyMatchFollowUpDto>>
            GetLeadFollowUpsAsync(
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
                .OrderByDescending(x => x.MatchScore)
                .Take(limit)
                .Select(CreateFollowUp)
                .ToList();
        }

        public async Task<PropertyMatchFollowUpDto?>
            GetNextFollowUpAsync(int leadId)
        {
            var followUps =
                await GetLeadFollowUpsAsync(
                    leadId,
                    100);

            return followUps
                .OrderBy(x => x.FollowUpAt)
                .ThenByDescending(x => x.MatchScore)
                .FirstOrDefault();
        }

        private static PropertyMatchFollowUpDto
            CreateFollowUp(
                PropertyCustomerMatchDto match)
        {
            return new PropertyMatchFollowUpDto
            {
                MatchId = match.Id,
                PropertyId = match.PropertyId,
                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                CurrentStage = match.Status,

                Priority = GetPriority(
                    match.MatchScore,
                    match.Status),

                RecommendedAction =
                    GetRecommendedAction(
                        match.MatchScore,
                        match.Status),

                FollowUpAt =
                    GetFollowUpDate(match),

                Reason =
                    match.MatchReason,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static DateTime GetFollowUpDate(
            PropertyCustomerMatchDto match)
        {
            var baseDate =
                match.UpdatedAt ??
                match.CreatedAt;

            var hours = match.MatchScore switch
            {
                >= 90 => 2,
                >= 80 => 6,
                >= 70 => 12,
                >= 60 => 24,
                _ => 48
            };

            return baseDate.AddHours(hours);
        }

        private static string GetPriority(
            decimal score,
            string status)
        {
            if (string.Equals(
                    status,
                    "Offer",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Critical";
            }

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
                return "Teklifi takip et ve müşteriyi ara.";
            }

            if (string.Equals(
                    status,
                    "Meeting",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Toplantı sonrası geri dönüş yap.";
            }

            if (string.Equals(
                    status,
                    "Contacted",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Müşterinin ilgisini doğrula ve görüşme planla.";
            }

            if (string.Equals(
                    status,
                    "Viewed",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Müşteriyle iletişime geç.";
            }

            if (score >= 90)
                return "Müşteriyi hemen ara.";

            if (score >= 75)
                return "Bugün müşteriyle iletişime geç.";

            if (score >= 60)
                return "İlanı müşteriye gönder ve geri dönüş planla.";

            return "Eşleşmeyi incele.";
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

    public class PropertyMatchFollowUpDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public string CurrentStage { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;

        public DateTime FollowUpAt { get; set; }

        public string Reason { get; set; }
            = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }
}
