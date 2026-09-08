using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchConversionService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        private static readonly string[] ValidStages =
        {
            "New",
            "Viewed",
            "Contacted",
            "Meeting",
            "Offer",
            "Won",
            "Lost"
        };

        public PropertyCustomerMatchConversionService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<PropertyCustomerMatchDto?> ChangeStageAsync(
            int matchId,
            string stage)
        {
            if (matchId <= 0)
                throw new ArgumentOutOfRangeException(nameof(matchId));

            if (string.IsNullOrWhiteSpace(stage))
                throw new ArgumentException(
                    "Aşama boş olamaz.",
                    nameof(stage));

            var normalizedStage = NormalizeStage(stage);

            if (normalizedStage == null)
                throw new ArgumentException(
                    $"Geçersiz aşama: {stage}",
                    nameof(stage));

            var match = await _matchService.GetByIdAsync(matchId);

            if (match == null)
                return null;

            match.Status = normalizedStage;
            match.UpdatedAt = DateTime.UtcNow;

            return await _matchService.UpdateAsync(
                matchId,
                match);
        }

        public async Task<PropertyMatchConversionAnalyticsDto>
            GetAnalyticsAsync()
        {
            var matches = await _matchService.GetAllAsync();

            var total = matches.Count;

            var newCount = CountStage(matches, "New");
            var viewed = CountStage(matches, "Viewed");
            var contacted = CountStage(matches, "Contacted");
            var meeting = CountStage(matches, "Meeting");
            var offer = CountStage(matches, "Offer");
            var won = CountStage(matches, "Won");
            var lost = CountStage(matches, "Lost");

            var completed = won + lost;

            return new PropertyMatchConversionAnalyticsDto
            {
                TotalMatches = total,

                NewCount = newCount,

                ViewedCount = viewed,

                ContactedCount = contacted,

                MeetingCount = meeting,

                OfferCount = offer,

                WonCount = won,

                LostCount = lost,

                ContactRate = CalculateRate(
                    contacted + meeting + offer + won + lost,
                    total),

                MeetingRate = CalculateRate(
                    meeting + offer + won + lost,
                    total),

                OfferRate = CalculateRate(
                    offer + won + lost,
                    total),

                WinRate = CalculateRate(
                    won,
                    completed),

                OverallConversionRate = CalculateRate(
                    won,
                    total),

                GeneratedAt = DateTime.UtcNow
            };
        }

        public async Task<List<PropertyCustomerMatchDto>>
            GetByStageAsync(string stage)
        {
            var normalizedStage = NormalizeStage(stage);

            if (normalizedStage == null)
                throw new ArgumentException(
                    $"Geçersiz aşama: {stage}",
                    nameof(stage));

            var matches = await _matchService.GetAllAsync();

            return matches
                .Where(x =>
                    string.Equals(
                        x.Status,
                        normalizedStage,
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.MatchScore)
                .ThenByDescending(x => x.UpdatedAt ?? x.CreatedAt)
                .ToList();
        }

        public IReadOnlyList<string> GetValidStages()
        {
            return ValidStages;
        }

        private static int CountStage(
            IEnumerable<PropertyCustomerMatchDto> matches,
            string stage)
        {
            return matches.Count(x =>
                string.Equals(
                    x.Status,
                    stage,
                    StringComparison.OrdinalIgnoreCase));
        }

        private static string? NormalizeStage(string stage)
        {
            return ValidStages.FirstOrDefault(x =>
                string.Equals(
                    x,
                    stage.Trim(),
                    StringComparison.OrdinalIgnoreCase));
        }

        private static decimal CalculateRate(
            int value,
            int total)
        {
            if (total <= 0)
                return 0;

            return Math.Round(
                (decimal)value / total * 100,
                2);
        }
    }

    public class PropertyMatchConversionAnalyticsDto
    {
        public int TotalMatches { get; set; }

        public int NewCount { get; set; }

        public int ViewedCount { get; set; }

        public int ContactedCount { get; set; }

        public int MeetingCount { get; set; }

        public int OfferCount { get; set; }

        public int WonCount { get; set; }

        public int LostCount { get; set; }

        public decimal ContactRate { get; set; }

        public decimal MeetingRate { get; set; }

        public decimal OfferRate { get; set; }

        public decimal WinRate { get; set; }

        public decimal OverallConversionRate { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
