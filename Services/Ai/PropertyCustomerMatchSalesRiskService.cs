using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesRiskService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchSalesRiskService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchSalesRiskDto>>
            GetRisksAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Where(x => !IsWon(x.Status))
                .Select(CreateRisk)
                .OrderByDescending(x => x.RiskScore)
                .ThenBy(x => x.MatchScore)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchSalesRiskDto?>
            GetMatchRiskAsync(int matchId)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var match = await _matchService.GetByIdAsync(matchId);

            if (match == null)
                return null;

            return CreateRisk(match);
        }

        public async Task<List<PropertyMatchSalesRiskDto>>
            GetLeadRisksAsync(
                int leadId,
                int limit = 20)
        {
            if (leadId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(leadId));
            }

            var matches = await _matchService
                .GetByLeadIdAsync(leadId);

            return matches
                .Where(x => !IsWon(x.Status))
                .Select(CreateRisk)
                .OrderByDescending(x => x.RiskScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchSalesRiskDto>>
            GetPropertyRisksAsync(
                int propertyId,
                int limit = 20)
        {
            if (propertyId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propertyId));
            }

            var matches = await _matchService
                .GetByPropertyIdAsync(propertyId);

            return matches
                .Where(x => !IsWon(x.Status))
                .Select(CreateRisk)
                .OrderByDescending(x => x.RiskScore)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchSalesRiskSummaryDto>
            GetSummaryAsync()
        {
            var risks = await GetRisksAsync(100);

            var averageRisk =
                risks.Count == 0
                    ? 0
                    : risks.Average(x => x.RiskScore);

            return new PropertyMatchSalesRiskSummaryDto
            {
                Total = risks.Count,

                Critical = risks.Count(
                    x => x.RiskLevel == "Critical"),

                VeryHigh = risks.Count(
                    x => x.RiskLevel == "VeryHigh"),

                High = risks.Count(
                    x => x.RiskLevel == "High"),

                Medium = risks.Count(
                    x => x.RiskLevel == "Medium"),

                Low = risks.Count(
                    x => x.RiskLevel == "Low"),

                AverageRiskScore =
                    Math.Round(averageRisk, 2),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static PropertyMatchSalesRiskDto
            CreateRisk(PropertyCustomerMatchDto match)
        {
            decimal risk = 0;

            var reasons = new List<string>();

            if (IsLost(match.Status))
            {
                risk = 100;

                reasons.Add(
                    "Satış kaybedilmiş durumda.");
            }
            else
            {
                AddMatchScoreRisk(
                    match,
                    ref risk,
                    reasons);

                AddCriteriaRisks(
                    match,
                    ref risk,
                    reasons);

                AddStageRisk(
                    match.Status,
                    ref risk,
                    reasons);

                AddAgeRisk(
                    match,
                    ref risk,
                    reasons);
            }

            risk = Math.Clamp(
                risk,
                0,
                100);

            var level =
                GetRiskLevel(risk);

            return new PropertyMatchSalesRiskDto
            {
                MatchId = match.Id,

                PropertyId = match.PropertyId,

                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                Stage = match.Status,

                RiskScore =
                    Math.Round(risk, 2),

                RiskLevel = level,

                RiskReasons =
                    reasons.Count == 0
                        ? "Belirgin satış riski bulunamadı."
                        : string.Join(
                            ", ",
                            reasons),

                RecommendedAction =
                    GetRecommendedAction(
                        level,
                        match),

                RequiresImmediateAction =
                    risk >= 80,

                CalculatedAt =
                    DateTime.UtcNow
            };
        }

        private static void AddMatchScoreRisk(
            PropertyCustomerMatchDto match,
            ref decimal risk,
            List<string> reasons)
        {
            if (match.MatchScore < 40)
            {
                risk += 30;

                reasons.Add(
                    "Eşleşme skoru çok düşük.");
            }
            else if (match.MatchScore < 60)
            {
                risk += 20;

                reasons.Add(
                    "Eşleşme skoru düşük.");
            }
            else if (match.MatchScore < 75)
            {
                risk += 10;

                reasons.Add(
                    "Eşleşme skoru orta seviyede.");
            }
        }

        private static void AddCriteriaRisks(
            PropertyCustomerMatchDto match,
            ref decimal risk,
            List<string> reasons)
        {
            if (!match.PriceMatched)
            {
                risk += 15;
                reasons.Add("Bütçe/fiyat uyumsuzluğu.");
            }

            if (!match.LocationMatched)
            {
                risk += 15;
                reasons.Add("Lokasyon uyumsuzluğu.");
            }

            if (!match.PropertyTypeMatched)
            {
                risk += 10;
                reasons.Add("Gayrimenkul türü uyumsuzluğu.");
            }

            if (!match.RoomCountMatched)
            {
                risk += 8;
                reasons.Add("Oda sayısı uyumsuzluğu.");
            }

            if (!match.SizeMatched)
            {
                risk += 8;
                reasons.Add("Metrekare uyumsuzluğu.");
            }
        }

        private static void AddStageRisk(
            string status,
            ref decimal risk,
            List<string> reasons)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                risk += 10;
                reasons.Add("Satış aşaması belirlenmemiş.");
                return;
            }

            switch (status.Trim().ToLowerInvariant())
            {
                case "new":
                    risk += 12;
                    reasons.Add(
                        "Müşteriyle henüz aktif temas kurulmamış.");
                    break;

                case "viewed":
                    risk += 8;
                    break;

                case "contacted":
                    risk += 5;
                    break;

                case "meeting":
                    risk += 2;
                    break;

                case "offer":
                    risk -= 5;
                    break;
            }
        }

        private static void AddAgeRisk(
            PropertyCustomerMatchDto match,
            ref decimal risk,
            List<string> reasons)
        {
            var referenceDate =
                match.UpdatedAt ??
                match.CreatedAt;

            var age =
                DateTime.UtcNow - referenceDate;

            if (age.TotalDays >= 14)
            {
                risk += 20;

                reasons.Add(
                    "Eşleşme 14 günden uzun süredir ilerlemiyor.");
            }
            else if (age.TotalDays >= 7)
            {
                risk += 12;

                reasons.Add(
                    "Eşleşme bir haftadır ilerlemiyor.");
            }
            else if (age.TotalDays >= 3)
            {
                risk += 6;

                reasons.Add(
                    "Takip gecikmesi riski bulunuyor.");
            }
        }

        private static string GetRiskLevel(
            decimal risk)
        {
            return risk switch
            {
                >= 90 => "Critical",
                >= 75 => "VeryHigh",
                >= 60 => "High",
                >= 40 => "Medium",
                _ => "Low"
            };
        }

        private static string GetRecommendedAction(
            string level,
            PropertyCustomerMatchDto match)
        {
            if (IsLost(match.Status))
            {
                return
                    "Kaybedilme nedenini analiz et ve CRM'e kaydet.";
            }

            if (!match.PriceMatched)
            {
                return
                    "Müşterinin bütçesine uygun alternatif ilan sun.";
            }

            if (!match.LocationMatched)
            {
                return
                    "Alternatif lokasyonları müşteriyle değerlendir.";
            }

            return level switch
            {
                "Critical" =>
                    "Müşteriyle hemen iletişime geç.",

                "VeryHigh" =>
                    "Bugün satış temsilcisi takibi yap.",

                "High" =>
                    "Müşteriyi ara ve ihtiyaçlarını tekrar doğrula.",

                "Medium" =>
                    "Takip planı oluştur.",

                _ =>
                    "Normal satış sürecine devam et."
            };
        }

        private static bool IsWon(string status)
        {
            return string.Equals(
                status,
                "Won",
                StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsLost(string status)
        {
            return string.Equals(
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

    public class PropertyMatchSalesRiskDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal RiskScore { get; set; }

        public string RiskLevel { get; set; }
            = string.Empty;

        public string RiskReasons { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;

        public bool RequiresImmediateAction { get; set; }

        public DateTime CalculatedAt { get; set; }
    }

    public class PropertyMatchSalesRiskSummaryDto
    {
        public int Total { get; set; }

        public int Critical { get; set; }

        public int VeryHigh { get; set; }

        public int High { get; set; }

        public int Medium { get; set; }

        public int Low { get; set; }

        public decimal AverageRiskScore { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
