namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesAutomationService
    {
        private readonly
            PropertyCustomerMatchSalesNextBestActionService
                _nextBestActionService;

        public PropertyCustomerMatchSalesAutomationService(
            PropertyCustomerMatchSalesNextBestActionService
                nextBestActionService)
        {
            _nextBestActionService = nextBestActionService;
        }

        public async Task<List<PropertyMatchSalesAutomationDto>>
            GetAutomationsAsync(int limit = 50)
        {
            var actions =
                await _nextBestActionService
                    .GetActionsAsync(100);

            return actions
                .Where(x =>
                    !string.Equals(
                        x.NextBestAction,
                        "Wait",
                        StringComparison.OrdinalIgnoreCase))
                .Select(CreateAutomation)
                .OrderByDescending(x => x.AutomationScore)
                .ThenBy(x => x.ExecuteAt)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchSalesAutomationDto?>
            GetForMatchAsync(int matchId)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var action =
                await _nextBestActionService
                    .GetForMatchAsync(matchId);

            if (action == null)
                return null;

            if (string.Equals(
                action.NextBestAction,
                "Wait",
                StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return CreateAutomation(action);
        }

        public async Task<List<PropertyMatchSalesAutomationDto>>
            GetForLeadAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            var actions =
                await _nextBestActionService
                    .GetForLeadAsync(
                        leadId,
                        100);

            return actions
                .Where(x =>
                    !string.Equals(
                        x.NextBestAction,
                        "Wait",
                        StringComparison.OrdinalIgnoreCase))
                .Select(CreateAutomation)
                .OrderByDescending(x => x.AutomationScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesAutomationDto>>
            GetReadyAutomationsAsync(
                int limit = 20)
        {
            var automations =
                await GetAutomationsAsync(100);

            return automations
                .Where(x =>
                    x.Status == "Ready" ||
                    x.Status == "Urgent")
                .OrderByDescending(x => x.AutomationScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesAutomationDto>>
            GetUrgentAutomationsAsync(
                int limit = 20)
        {
            var automations =
                await GetAutomationsAsync(100);

            return automations
                .Where(x =>
                    x.Status == "Urgent")
                .OrderByDescending(x => x.AutomationScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesAutomationDto>>
            GetDueWithinAsync(
                int hours,
                int limit = 20)
        {
            hours = Math.Clamp(
                hours,
                1,
                168);

            var automations =
                await GetAutomationsAsync(100);

            var maximumDate =
                DateTime.UtcNow.AddHours(hours);

            return automations
                .Where(x =>
                    x.ExecuteAt <= maximumDate)
                .OrderBy(x => x.ExecuteAt)
                .ThenByDescending(x => x.AutomationScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchSalesAutomationSummaryDto>
            GetSummaryAsync()
        {
            var automations =
                await GetAutomationsAsync(100);

            if (automations.Count == 0)
            {
                return new PropertyMatchSalesAutomationSummaryDto
                {
                    GeneratedAt =
                        DateTime.UtcNow
                };
            }

            return new PropertyMatchSalesAutomationSummaryDto
            {
                TotalAutomations =
                    automations.Count,

                UrgentAutomations =
                    automations.Count(
                        x => x.Status == "Urgent"),

                ReadyAutomations =
                    automations.Count(
                        x => x.Status == "Ready"),

                ScheduledAutomations =
                    automations.Count(
                        x => x.Status == "Scheduled"),

                CallAutomations =
                    automations.Count(
                        x => x.AutomationType == "Call"),

                MeetingAutomations =
                    automations.Count(
                        x => x.AutomationType == "Meeting"),

                OfferAutomations =
                    automations.Count(
                        x => x.AutomationType == "Offer"),

                FollowUpAutomations =
                    automations.Count(
                        x => x.AutomationType == "FollowUp"),

                AlternativePropertyAutomations =
                    automations.Count(
                        x =>
                            x.AutomationType ==
                            "AlternativeProperty"),

                AverageAutomationScore =
                    Math.Round(
                        automations.Average(
                            x => x.AutomationScore),
                        2),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static PropertyMatchSalesAutomationDto
            CreateAutomation(
                PropertyMatchNextBestActionDto action)
        {
            var automationType =
                GetAutomationType(
                    action.NextBestAction);

            var automationScore =
                CalculateAutomationScore(action);

            var executeAt =
                DateTime.UtcNow.AddHours(
                    Math.Max(
                        1,
                        action.ExecuteWithinHours));

            var status =
                GetAutomationStatus(
                    action,
                    automationScore);

            return new PropertyMatchSalesAutomationDto
            {
                MatchId =
                    action.MatchId,

                LeadId =
                    action.LeadId,

                PropertyId =
                    action.PropertyId,

                Stage =
                    action.Stage,

                AutomationType =
                    automationType,

                Action =
                    action.NextBestAction,

                ActionTitle =
                    action.ActionTitle,

                Channel =
                    action.RecommendedChannel,

                AutomationScore =
                    Math.Round(
                        automationScore,
                        2),

                UrgencyScore =
                    action.UrgencyScore,

                Urgency =
                    action.Urgency,

                Status =
                    status,

                ExecuteAt =
                    executeAt,

                ExecuteWithinHours =
                    action.ExecuteWithinHours,

                RequiresApproval =
                    RequiresApproval(
                        action.NextBestAction),

                AutoExecutable =
                    IsAutoExecutable(
                        action.NextBestAction),

                Instruction =
                    BuildInstruction(action),

                Reason =
                    action.Reason,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static decimal CalculateAutomationScore(
            PropertyMatchNextBestActionDto action)
        {
            var actionWeight =
                GetActionWeight(
                    action.NextBestAction);

            var score =
                (action.ActionScore * 0.45m) +
                (action.UrgencyScore * 0.25m) +
                (action.ConversionProbability * 0.15m) +
                (action.PriorityScore * 0.10m) +
                (actionWeight * 0.05m);

            if (action.RequiresImmediateAction)
            {
                score += 5;
            }

            return Math.Clamp(
                score,
                0,
                100);
        }

        private static string GetAutomationType(
            string action)
        {
            return action switch
            {
                "Call" =>
                    "Call",

                "ScheduleMeeting" =>
                    "Meeting",

                "SendOffer" =>
                    "Offer",

                "FollowUp" =>
                    "FollowUp",

                "SuggestAlternative" =>
                    "AlternativeProperty",

                _ =>
                    "General"
            };
        }

        private static decimal GetActionWeight(
            string action)
        {
            return action switch
            {
                "SendOffer" => 100,
                "ScheduleMeeting" => 90,
                "Call" => 80,
                "FollowUp" => 70,
                "SuggestAlternative" => 55,
                _ => 30
            };
        }

        private static string GetAutomationStatus(
            PropertyMatchNextBestActionDto action,
            decimal automationScore)
        {
            if (action.RequiresImmediateAction ||
                action.Urgency == "Critical")
            {
                return "Urgent";
            }

            if (automationScore >= 70 ||
                action.Urgency == "High")
            {
                return "Ready";
            }

            return "Scheduled";
        }

        private static bool RequiresApproval(
            string action)
        {
            return action switch
            {
                "SendOffer" => true,
                "ScheduleMeeting" => true,
                _ => false
            };
        }

        private static bool IsAutoExecutable(
            string action)
        {
            return action switch
            {
                "FollowUp" => true,
                "SuggestAlternative" => true,
                _ => false
            };
        }

        private static string BuildInstruction(
            PropertyMatchNextBestActionDto action)
        {
            return action.NextBestAction switch
            {
                "Call" =>
                    "Müşteriyi telefonla ara ve güncel ilgisini doğrula.",

                "ScheduleMeeting" =>
                    "Müşteri için satış görüşmesi veya ilan gösterimi planla.",

                "SendOffer" =>
                    "Müşteriye özel satış teklifini hazırla ve onay sonrası gönder.",

                "FollowUp" =>
                    "Müşteriye takip mesajı gönder ve satış sürecini ilerlet.",

                "SuggestAlternative" =>
                    "Müşterinin kriterlerine uygun alternatif ilanları otomatik hazırla.",

                _ =>
                    action.Recommendation
            };
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

    public class PropertyMatchSalesAutomationDto
    {
        public int MatchId { get; set; }

        public int LeadId { get; set; }

        public int PropertyId { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public string AutomationType { get; set; }
            = string.Empty;

        public string Action { get; set; }
            = string.Empty;

        public string ActionTitle { get; set; }
            = string.Empty;

        public string Channel { get; set; }
            = string.Empty;

        public decimal AutomationScore { get; set; }

        public decimal UrgencyScore { get; set; }

        public string Urgency { get; set; }
            = string.Empty;

        public string Status { get; set; }
            = string.Empty;

        public DateTime ExecuteAt { get; set; }

        public int ExecuteWithinHours { get; set; }

        public bool RequiresApproval { get; set; }

        public bool AutoExecutable { get; set; }

        public string Instruction { get; set; }
            = string.Empty;

        public string Reason { get; set; }
            = string.Empty;

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchSalesAutomationSummaryDto
    {
        public int TotalAutomations { get; set; }

        public int UrgentAutomations { get; set; }

        public int ReadyAutomations { get; set; }

        public int ScheduledAutomations { get; set; }

        public int CallAutomations { get; set; }

        public int MeetingAutomations { get; set; }

        public int OfferAutomations { get; set; }

        public int FollowUpAutomations { get; set; }

        public int AlternativePropertyAutomations { get; set; }

        public decimal AverageAutomationScore { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
