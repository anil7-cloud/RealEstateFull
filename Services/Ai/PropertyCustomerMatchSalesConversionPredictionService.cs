using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchSalesConversionPredictionService
    {
        private readonly IPropertyCustomerMatchService _matchService;

        public PropertyCustomerMatchSalesConversionPredictionService(
            IPropertyCustomerMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<List<PropertyMatchConversionPredictionDto>>
            GetPredictionsAsync(int limit = 50)
        {
            limit = NormalizeLimit(limit);

            var matches = await _matchService.GetAllAsync();

            return matches
                .Select(CreatePrediction)
                .OrderByDescending(x => x.ConversionProbability)
                .ThenByDescending(x => x.MatchScore)
                .Take(limit)
                .ToList();
        }

        public async Task<PropertyMatchConversionPredictionDto?>
            GetMatchPredictionAsync(int matchId)
        {
            if (matchId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(matchId));
            }

            var match = await _matchService.GetByIdAsync(matchId);

            if (match == null)
                return null;

            return CreatePrediction(match);
        }

        public async Task<List<PropertyMatchConversionPredictionDto>>
            GetLeadPredictionsAsync(
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
                .Select(CreatePrediction)
                .OrderByDescending(x => x.ConversionProbability)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<List<PropertyMatchConversionPredictionDto>>
            GetPropertyPredictionsAsync(
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
                .Select(CreatePrediction)
                .OrderByDescending(x => x.ConversionProbability)
                .Take(NormalizeLimit(limit))
                .ToList();
        }

        public async Task<PropertyMatchConversionPredictionSummaryDto>
            GetSummaryAsync()
        {
            var matches = await _matchService.GetAllAsync();

            var predictions = matches
                .Select(CreatePrediction)
                .ToList();

            if (predictions.Count == 0)
            {
                return new PropertyMatchConversionPredictionSummaryDto
                {
                    GeneratedAt = DateTime.UtcNow
                };
            }

            return new PropertyMatchConversionPredictionSummaryDto
            {
                TotalPredictions =
                    predictions.Count,

                VeryHighProbability =
                    predictions.Count(x =>
                        x.PredictionLevel == "VeryHigh"),

                HighProbability =
                    predictions.Count(x =>
                        x.PredictionLevel == "High"),

                MediumProbability =
                    predictions.Count(x =>
                        x.PredictionLevel == "Medium"),

                LowProbability =
                    predictions.Count(x =>
                        x.PredictionLevel == "Low"),

                VeryLowProbability =
                    predictions.Count(x =>
                        x.PredictionLevel == "VeryLow"),

                AverageConversionProbability =
                    Math.Round(
                        predictions.Average(
                            x => x.ConversionProbability),
                        2),

                ExpectedConversions =
                    Math.Round(
                        predictions.Sum(
                            x => x.ConversionProbability) / 100m,
                        2),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static PropertyMatchConversionPredictionDto
            CreatePrediction(PropertyCustomerMatchDto match)
        {
            if (IsStatus(match.Status, "Won"))
            {
                return CreateFinalPrediction(
                    match,
                    100,
                    "VeryHigh",
                    "Satış kazanılmış durumda.");
            }

            if (IsStatus(match.Status, "Lost"))
            {
                return CreateFinalPrediction(
                    match,
                    0,
                    "VeryLow",
                    "Satış kaybedilmiş durumda.");
            }

            decimal probability = 0;

            var reasons = new List<string>();

            // Eşleşme skorunun ağırlığı: %55
            probability += match.MatchScore * 0.55m;

            AddCriteriaScore(
                match,
                ref probability,
                reasons);

            AddStageScore(
                match.Status,
                ref probability,
                reasons);

            AddFreshnessScore(
                match,
                ref probability,
                reasons);

            probability = Math.Clamp(
                probability,
                0,
                95);

            probability = Math.Round(
                probability,
                2);

            var level =
                GetPredictionLevel(probability);

            return new PropertyMatchConversionPredictionDto
            {
                MatchId = match.Id,

                PropertyId = match.PropertyId,

                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                Stage = match.Status,

                ConversionProbability =
                    probability,

                PredictionLevel =
                    level,

                Confidence =
                    CalculateConfidence(match),

                Reasons =
                    reasons.Count == 0
                        ? "Standart eşleşme verileri kullanıldı."
                        : string.Join(
                            ", ",
                            reasons),

                RecommendedAction =
                    GetRecommendedAction(
                        probability,
                        match.Status),

                PredictedToConvert =
                    probability >= 65,

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static void AddCriteriaScore(
            PropertyCustomerMatchDto match,
            ref decimal probability,
            List<string> reasons)
        {
            if (match.PriceMatched)
            {
                probability += 6;
                reasons.Add("Fiyat uyumlu");
            }

            if (match.LocationMatched)
            {
                probability += 6;
                reasons.Add("Lokasyon uyumlu");
            }

            if (match.PropertyTypeMatched)
            {
                probability += 4;
                reasons.Add("Gayrimenkul türü uyumlu");
            }

            if (match.RoomCountMatched)
            {
                probability += 3;
                reasons.Add("Oda sayısı uyumlu");
            }

            if (match.SizeMatched)
            {
                probability += 3;
                reasons.Add("Metrekare uyumlu");
            }
        }

        private static void AddStageScore(
            string status,
            ref decimal probability,
            List<string> reasons)
        {
            if (string.IsNullOrWhiteSpace(status))
                return;

            switch (status.Trim().ToLowerInvariant())
            {
                case "offer":
                    probability += 18;
                    reasons.Add("Teklif aşamasında");
                    break;

                case "meeting":
                    probability += 14;
                    reasons.Add("Görüşme aşamasında");
                    break;

                case "contacted":
                    probability += 10;
                    reasons.Add("Müşteriyle iletişim kuruldu");
                    break;

                case "viewed":
                    probability += 6;
                    reasons.Add("İlan görüntülendi");
                    break;

                case "new":
                    probability += 2;
                    reasons.Add("Yeni satış fırsatı");
                    break;
            }
        }

        private static void AddFreshnessScore(
            PropertyCustomerMatchDto match,
            ref decimal probability,
            List<string> reasons)
        {
            var referenceDate =
                match.UpdatedAt ??
                match.CreatedAt;

            var age =
                DateTime.UtcNow - referenceDate;

            if (age.TotalHours <= 24)
            {
                probability += 5;
                reasons.Add("Çok güncel eşleşme");
            }
            else if (age.TotalDays <= 3)
            {
                probability += 3;
                reasons.Add("Güncel eşleşme");
            }
            else if (age.TotalDays >= 14)
            {
                probability -= 8;
                reasons.Add("Uzun süredir ilerleme yok");
            }
            else if (age.TotalDays >= 7)
            {
                probability -= 4;
                reasons.Add("Takip gecikmesi bulunuyor");
            }
        }

        private static decimal CalculateConfidence(
            PropertyCustomerMatchDto match)
        {
            var criteria =
                CountMatchedCriteria(match);

            decimal confidence =
                50 + (criteria * 8);

            if (!string.IsNullOrWhiteSpace(match.Status))
                confidence += 5;

            if (match.MatchScore > 0)
                confidence += 5;

            return Math.Round(
                Math.Clamp(
                    confidence,
                    0,
                    100),
                2);
        }

        private static int CountMatchedCriteria(
            PropertyCustomerMatchDto match)
        {
            var count = 0;

            if (match.PriceMatched)
                count++;

            if (match.LocationMatched)
                count++;

            if (match.PropertyTypeMatched)
                count++;

            if (match.RoomCountMatched)
                count++;

            if (match.SizeMatched)
                count++;

            return count;
        }

        private static string GetPredictionLevel(
            decimal probability)
        {
            return probability switch
            {
                >= 85 => "VeryHigh",
                >= 70 => "High",
                >= 50 => "Medium",
                >= 30 => "Low",
                _ => "VeryLow"
            };
        }

        private static string GetRecommendedAction(
            decimal probability,
            string status)
        {
            if (IsStatus(status, "Offer"))
            {
                return
                    "Teklifi takip et ve satış kapanışına odaklan.";
            }

            if (probability >= 85)
            {
                return
                    "Müşteriyle hemen iletişime geç ve teklif sürecini başlat.";
            }

            if (probability >= 70)
            {
                return
                    "Satış görüşmesi planla.";
            }

            if (probability >= 50)
            {
                return
                    "İlan detaylarını gönder ve müşterinin ilgisini doğrula.";
            }

            if (probability >= 30)
            {
                return
                    "Müşteri ihtiyaçlarını yeniden değerlendir.";
            }

            return
                "Daha uygun alternatif ilanlar öner.";
        }

        private static PropertyMatchConversionPredictionDto
            CreateFinalPrediction(
                PropertyCustomerMatchDto match,
                decimal probability,
                string level,
                string reason)
        {
            return new PropertyMatchConversionPredictionDto
            {
                MatchId = match.Id,

                PropertyId = match.PropertyId,

                LeadId = match.LeadId,

                MatchScore = match.MatchScore,

                Stage = match.Status,

                ConversionProbability =
                    probability,

                PredictionLevel =
                    level,

                Confidence = 100,

                Reasons = reason,

                RecommendedAction =
                    IsStatus(match.Status, "Won")
                        ? "Satış tamamlandı."
                        : "Kaybedilme nedenini analiz et.",

                PredictedToConvert =
                    IsStatus(match.Status, "Won"),

                GeneratedAt =
                    DateTime.UtcNow
            };
        }

        private static bool IsStatus(
            string status,
            string expected)
        {
            return string.Equals(
                status,
                expected,
                StringComparison.OrdinalIgnoreCase);
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }

    public class PropertyMatchConversionPredictionDto
    {
        public int MatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal MatchScore { get; set; }

        public string Stage { get; set; }
            = string.Empty;

        public decimal ConversionProbability { get; set; }

        public string PredictionLevel { get; set; }
            = string.Empty;

        public decimal Confidence { get; set; }

        public string Reasons { get; set; }
            = string.Empty;

        public string RecommendedAction { get; set; }
            = string.Empty;

        public bool PredictedToConvert { get; set; }

        public DateTime GeneratedAt { get; set; }
    }

    public class PropertyMatchConversionPredictionSummaryDto
    {
        public int TotalPredictions { get; set; }

        public int VeryHighProbability { get; set; }

        public int HighProbability { get; set; }

        public int MediumProbability { get; set; }

        public int LowProbability { get; set; }

        public int VeryLowProbability { get; set; }

        public decimal AverageConversionProbability { get; set; }

        public decimal ExpectedConversions { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}
