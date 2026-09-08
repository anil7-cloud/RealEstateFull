namespace REAL_ESTATE_CLEAN.Services.Ai
{
    public class PropertyCustomerMatchScoringService
    {
        private const decimal PriceWeight = 30m;
        private const decimal LocationWeight = 25m;
        private const decimal PropertyTypeWeight = 15m;
        private const decimal RoomCountWeight = 15m;
        private const decimal SizeWeight = 15m;

        public PropertyMatchScoreResult Calculate(
            PropertyMatchScoreRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            decimal score = 0;

            var matchedCriteria = new List<string>();
            var missingCriteria = new List<string>();

            EvaluateCriterion(
                request.PriceMatched,
                PriceWeight,
                "Fiyat bütçeye uygun",
                "Fiyat bütçeyle uyuşmuyor",
                ref score,
                matchedCriteria,
                missingCriteria);

            EvaluateCriterion(
                request.LocationMatched,
                LocationWeight,
                "Konum tercihiyle eşleşiyor",
                "Konum tercihiyle uyuşmuyor",
                ref score,
                matchedCriteria,
                missingCriteria);

            EvaluateCriterion(
                request.PropertyTypeMatched,
                PropertyTypeWeight,
                "Gayrimenkul tipi uygun",
                "Gayrimenkul tipi uygun değil",
                ref score,
                matchedCriteria,
                missingCriteria);

            EvaluateCriterion(
                request.RoomCountMatched,
                RoomCountWeight,
                "Oda sayısı uygun",
                "Oda sayısı beklentiyle uyuşmuyor",
                ref score,
                matchedCriteria,
                missingCriteria);

            EvaluateCriterion(
                request.SizeMatched,
                SizeWeight,
                "Metrekare beklentisi uygun",
                "Metrekare beklentisiyle uyuşmuyor",
                ref score,
                matchedCriteria,
                missingCriteria);

            score = Math.Clamp(score, 0m, 100m);

            return new PropertyMatchScoreResult
            {
                Score = score,
                MatchLevel = GetMatchLevel(score),
                Reason = CreateReason(matchedCriteria),
                Recommendation = GetRecommendation(score),
                MatchedCriteria = matchedCriteria,
                MissingCriteria = missingCriteria,
                MatchedCriteriaCount = matchedCriteria.Count,
                TotalCriteriaCount = 5
            };
        }

        private static void EvaluateCriterion(
            bool matched,
            decimal weight,
            string matchedText,
            string missingText,
            ref decimal score,
            List<string> matchedCriteria,
            List<string> missingCriteria)
        {
            if (matched)
            {
                score += weight;
                matchedCriteria.Add(matchedText);
                return;
            }

            missingCriteria.Add(missingText);
        }

        private static string CreateReason(
            IReadOnlyCollection<string> matchedCriteria)
        {
            if (matchedCriteria.Count == 0)
                return "Belirgin eşleşme bulunamadı.";

            return string.Join(", ", matchedCriteria);
        }

        private static string GetMatchLevel(decimal score)
        {
            return score switch
            {
                >= 90 => "Excellent",
                >= 75 => "VeryGood",
                >= 60 => "Good",
                >= 40 => "Medium",
                _ => "Low"
            };
        }

        private static string GetRecommendation(decimal score)
        {
            return score switch
            {
                >= 90 =>
                    "Çok güçlü eşleşme. Müşteriyle öncelikli olarak iletişime geçin.",

                >= 75 =>
                    "Güçlü eşleşme. Portföyü müşteriye kısa sürede önerin.",

                >= 60 =>
                    "İyi eşleşme. Müşteriye alternatif portföy olarak sunulabilir.",

                >= 40 =>
                    "Orta düzey eşleşme. Eksik kriterleri kontrol ederek değerlendirin.",

                _ =>
                    "Zayıf eşleşme. Daha uygun portföylerin değerlendirilmesi önerilir."
            };
        }
    }

    public class PropertyMatchScoreRequest
    {
        public bool PriceMatched { get; set; }

        public bool LocationMatched { get; set; }

        public bool PropertyTypeMatched { get; set; }

        public bool RoomCountMatched { get; set; }

        public bool SizeMatched { get; set; }
    }

    public class PropertyMatchScoreResult
    {
        public decimal Score { get; set; }

        public string MatchLevel { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public string Recommendation { get; set; } = string.Empty;

        public List<string> MatchedCriteria { get; set; } = new();

        public List<string> MissingCriteria { get; set; } = new();

        public int MatchedCriteriaCount { get; set; }

        public int TotalCriteriaCount { get; set; }
    }
}
