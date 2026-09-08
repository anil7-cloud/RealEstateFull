using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchNotificationService
    {
        public PropertyMatchNotificationDto?
            CreateHighScoreNotification(
                PropertyCustomerMatchDto match)
        {
            ArgumentNullException.ThrowIfNull(match);

            if (match.MatchScore < 75)
                return null;

            var priority = match.MatchScore switch
            {
                >= 90 => "Critical",
                >= 80 => "High",
                _ => "Normal"
            };

            return new PropertyMatchNotificationDto
            {
                PropertyCustomerMatchId = match.Id,
                PropertyId = match.PropertyId,
                LeadId = match.LeadId,

                Type = "HighMatchScore",

                Priority = priority,

                Title = "Yüksek müşteri-ilan eşleşmesi",

                Message =
                    $"Lead #{match.LeadId} ile " +
                    $"Property #{match.PropertyId} arasında " +
                    $"%{match.MatchScore:N0} eşleşme bulundu.",

                MatchScore = match.MatchScore,

                CreatedAt = DateTime.UtcNow
            };
        }

        public PropertyMatchNotificationDto
            CreateStageChangeNotification(
                PropertyCustomerMatchDto match,
                string previousStage,
                string newStage)
        {
            ArgumentNullException.ThrowIfNull(match);

            return new PropertyMatchNotificationDto
            {
                PropertyCustomerMatchId = match.Id,
                PropertyId = match.PropertyId,
                LeadId = match.LeadId,

                Type = "StageChanged",

                Priority = GetStagePriority(newStage),

                Title = "Eşleşme satış aşaması değişti",

                Message =
                    $"Lead #{match.LeadId} / " +
                    $"Property #{match.PropertyId} eşleşmesi " +
                    $"{previousStage} aşamasından " +
                    $"{newStage} aşamasına geçti.",

                MatchScore = match.MatchScore,

                CreatedAt = DateTime.UtcNow
            };
        }

        public PropertyMatchNotificationDto?
            CreateHotLeadNotification(
                PropertyCustomerMatchDto match)
        {
            ArgumentNullException.ThrowIfNull(match);

            if (match.MatchScore < 90)
                return null;

            return new PropertyMatchNotificationDto
            {
                PropertyCustomerMatchId = match.Id,
                PropertyId = match.PropertyId,
                LeadId = match.LeadId,

                Type = "HotLead",

                Priority = "Critical",

                Title = "Sıcak müşteri fırsatı",

                Message =
                    $"Lead #{match.LeadId}, " +
                    $"Property #{match.PropertyId} için " +
                    $"%{match.MatchScore:N0} uyum skoruna sahip.",

                MatchScore = match.MatchScore,

                CreatedAt = DateTime.UtcNow
            };
        }

        public List<PropertyMatchNotificationDto>
            CreateBatchNotifications(
                IEnumerable<PropertyCustomerMatchDto> matches)
        {
            ArgumentNullException.ThrowIfNull(matches);

            var notifications =
                new List<PropertyMatchNotificationDto>();

            foreach (var match in matches)
            {
                var notification =
                    CreateHighScoreNotification(match);

                if (notification != null)
                    notifications.Add(notification);
            }

            return notifications
                .OrderByDescending(x => x.MatchScore)
                .ThenByDescending(x => x.CreatedAt)
                .ToList();
        }

        private static string GetStagePriority(string stage)
        {
            if (string.IsNullOrWhiteSpace(stage))
                return "Normal";

            return stage.Trim().ToLowerInvariant() switch
            {
                "won" => "Critical",
                "offer" => "High",
                "meeting" => "High",
                "contacted" => "Normal",
                "lost" => "Low",
                _ => "Normal"
            };
        }
    }

    public class PropertyMatchNotificationDto
    {
        public int PropertyCustomerMatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Priority { get; set; } = "Normal";

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public decimal MatchScore { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
