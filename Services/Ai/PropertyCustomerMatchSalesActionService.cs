using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesActionService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchSalesActionService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchSalesActionDto>>
            GetActionsAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Where(x => !IsClosed(x.Status))
                .Select(CreateAction)
                .OrderByDescending(x => x.ActionScore)
                .ThenBy(x => x.ExecuteBefore)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchSalesActionDto?>
            GetActionForMatchAsync(int matchId)
        {
            if (matchId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));

            var match = await _matchService.GetByIdAsync(matchId);

            if (match == null)
                return null;

            return CreateAction(match);
        }

        public async Task<List<PropertyMatchSalesActionDto>>
            GetLeadActionsAsync(
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
                .Select(CreateAction)
                .OrderByDescending(x => x.ActionScore)
                .Take(limit)
                .ToList();
        }

        public async Task<List<PropertyMatchSalesActionDto>>
            GetPropertyActionsAsync(
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
                .Select(CreateAction)
                .OrderByDescending(x => x.ActionScore)
                .Take(limit)
                .ToList();
        }

        private static PropertyMatchSalesActionDto CreateAction(
            PropertyCustomerMatchDto match)
        {
            var stageScore = GetStageScore(match.Status);

            var actionScore =
                (match.MatchScore * 0.75m) +
                stageScore;

            actionScore = Math.Clamp(
                actionScore,
                0,
                100);

            var actionType =
                GetActionType(
                    match.Status,
                    actionScore);

            var executeBefore =
                CalculateDeadline(
                    actionScore,
                    match.Status);

            return new PropertyMatchSalesActionDto
            {
                MatchId = match.Id,
                PropertyId = match.PropertyId,
                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                ActionScore = Math.Round(
                    actionScore,
                    2),

                Stage = match.Status,

                ActionType = actionType,

                Priority =
                    GetPriority(actionScore),

                ActionTitle =
                    GetActionTitle(actionType),

                ActionDescription =
                    GetActionDescription(
                        actionType,
                        match),

                ExecuteBefore =
                    executeBefore,

                IsUrgent =
                    actionScore >= 85 ||
                    string.Equals(
                        match.Status,
                        "Offer",
                        StringComparison.OrdinalIgnoreCase),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static string GetActionType(
            string status,
            decimal score)
        {
            if (string.Equals(
                    status,
                    "Offer",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "FollowOffer";
            }

            if (string.Equals(
                    status,
                    "Meeting",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "ScheduleFollowUp";
            }

            if (string.Equals(
                    status,
                    "Contacted",
                    StringComparison.OrdinalIgnoreCase))
            {
                return score >= 75
                    ? "ScheduleMeeting"
                    : "SendMessage";
            }

            if (string.Equals(
                    status,
                    "Viewed",
                    StringComparison.OrdinalIgnoreCase))
            {
                return score >= 80
                    ? "Call"
                    : "SendEmail";
            }

            if (score >= 90)
                return "Call";

            if (score >= 80)
                return "Call";

            if (score >= 70)
                return "SendWhatsApp";

            if (score >= 60)
                return "SendEmail";

            return "Review";
        }

        private static string GetActionTitle(
            string actionType)
        {
            return actionType switch
            {
                "Call" =>
                    "Müşteriyi ara",

                "SendWhatsApp" =>
                    "WhatsApp mesajı gönder",

                "SendEmail" =>
                    "E-posta gönder",

                "SendMessage" =>
                    "Müşteriye mesaj gönder",

                "ScheduleMeeting" =>
                    "Görüşme planla",

                "ScheduleFollowUp" =>
                    "Takip görüşmesi planla",

                "FollowOffer" =>
                    "Teklifi takip et",

                _ =>
                    "Eşleşmeyi incele"
            };
        }

        private static string GetActionDescription(
            string actionType,
            PropertyCustomerMatchDto match)
        {
            var target =
                $"Lead #{match.LeadId} / " +
                $"Property #{match.PropertyId}";

            return actionType switch
            {
                "Call" =>
                    $"{target} için müşteriyi doğrudan ara.",

                "SendWhatsApp" =>
                    $"{target} için ilan detaylarını WhatsApp ile gönder.",

                "SendEmail" =>
                    $"{target} için kişiselleştirilmiş ilan e-postası gönder.",

                "SendMessage" =>
                    $"{target} için müşterinin ilgisini doğrulayan mesaj gönder.",

                "ScheduleMeeting" =>
                    $"{target} için satış görüşmesi planla.",

                "ScheduleFollowUp" =>
                    $"{target} için görüşme sonrası takip oluştur.",

                "FollowOffer" =>
                    $"{target} için mevcut satış teklifini takip et.",

                _ =>
                    $"{target} eşleşmesini satış temsilcisi incelemeli."
            };
        }

        private static DateTime CalculateDeadline(
            decimal score,
            string status)
        {
            var now = DateTime.UtcNow;

            if (string.Equals(
                    status,
                    "Offer",
                    StringComparison.OrdinalIgnoreCase))
            {
                return now.AddHours(1);
            }

            return score switch
            {
                >= 90 => now.AddHours(1),
                >= 85 => now.AddHours(2),
                >= 75 => now.AddHours(6),
                >= 65 => now.AddHours(12),
                >= 50 => now.AddHours(24),
                _ => now.AddDays(2)
            };
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
                "offer" => 25,
                "meeting" => 20,
                "contacted" => 15,
                "viewed" => 10,
                "new" => 5,
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
                >= 55 => "Medium",
                _ => "Low"
            };
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

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }

    public class PropertyMatchSalesActionDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public decimal ActionScore { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public string ActionType { get; set; }
            = string.Empty;

        public string Priority { get; set; }
            = string.Empty;

        public string ActionTitle { get; set; }
            = string.Empty;

        public string ActionDescription { get; set; }
            = string.Empty;

        public DateTime ExecuteBefore { get; set; }

        public bool IsUrgent { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
