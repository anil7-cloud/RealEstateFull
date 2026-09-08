using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-conversion-prediction")]
    public class PropertyCustomerMatchSalesConversionPredictionController
        : ControllerBase
    {
        private readonly
            PropertyCustomerMatchSalesConversionPredictionService _service;

        public PropertyCustomerMatchSalesConversionPredictionController(
            PropertyCustomerMatchSalesConversionPredictionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result = await _service.GetPredictionsAsync(
                NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<
            ActionResult<PropertyMatchConversionPredictionDto>>
            GetForMatch(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            var result =
                await _service.GetMatchPredictionAsync(matchId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Eşleşme bulunamadı."
                });
            }

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetForLead(
                int leadId,
                [FromQuery] int limit = 20)
        {
            if (leadId <= 0)
            {
                return BadRequest(new
                {
                    message = "LeadId geçerli olmalıdır."
                });
            }

            var result =
                await _service.GetLeadPredictionsAsync(
                    leadId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetForProperty(
                int propertyId,
                [FromQuery] int limit = 20)
        {
            if (propertyId <= 0)
            {
                return BadRequest(new
                {
                    message = "PropertyId geçerli olmalıdır."
                });
            }

            var result =
                await _service.GetPropertyPredictionsAsync(
                    propertyId,
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<
            ActionResult<PropertyMatchConversionPredictionSummaryDto>>
            GetSummary()
        {
            var result = await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("high-probability")]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetHighProbability(
                [FromQuery] decimal minimumProbability = 70,
                [FromQuery] int limit = 30)
        {
            minimumProbability = Math.Clamp(
                minimumProbability,
                0,
                100);

            var predictions =
                await _service.GetPredictionsAsync(100);

            var result = predictions
                .Where(x =>
                    x.ConversionProbability >= minimumProbability)
                .OrderByDescending(
                    x => x.ConversionProbability)
                .ThenByDescending(
                    x => x.Confidence)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("predicted-conversions")]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetPredictedConversions(
                [FromQuery] int limit = 30)
        {
            var predictions =
                await _service.GetPredictionsAsync(100);

            var result = predictions
                .Where(x => x.PredictedToConvert)
                .OrderByDescending(
                    x => x.ConversionProbability)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("very-high")]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetVeryHigh(
                [FromQuery] int limit = 20)
        {
            var predictions =
                await _service.GetPredictionsAsync(100);

            var result = predictions
                .Where(x =>
                    string.Equals(
                        x.PredictionLevel,
                        "VeryHigh",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(
                    x => x.ConversionProbability)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("low-probability")]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetLowProbability(
                [FromQuery] decimal maximumProbability = 30,
                [FromQuery] int limit = 30)
        {
            maximumProbability = Math.Clamp(
                maximumProbability,
                0,
                100);

            var predictions =
                await _service.GetPredictionsAsync(100);

            var result = predictions
                .Where(x =>
                    x.ConversionProbability <= maximumProbability)
                .OrderBy(
                    x => x.ConversionProbability)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<
            ActionResult<List<PropertyMatchConversionPredictionDto>>>
            GetTop([FromQuery] int limit = 10)
        {
            var result =
                await _service.GetPredictionsAsync(
                    NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var summaryTask =
                _service.GetSummaryAsync();

            var predictionsTask =
                _service.GetPredictionsAsync(100);

            await Task.WhenAll(
                summaryTask,
                predictionsTask);

            var summary = await summaryTask;
            var predictions = await predictionsTask;

            var topPredictions = predictions
                .OrderByDescending(
                    x => x.ConversionProbability)
                .Take(10)
                .ToList();

            var likelyConversions = predictions
                .Where(x => x.PredictedToConvert)
                .OrderByDescending(
                    x => x.ConversionProbability)
                .Take(10)
                .ToList();

            return Ok(new
            {
                summary,

                topPredictions,

                likelyConversions,

                generatedAt =
                    DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesConversionPrediction",

                status = "Running",

                timestamp =
                    DateTime.UtcNow
            });
        }

        private static int NormalizeLimit(int limit)
        {
            if (limit < 1)
                return 10;

            return Math.Min(limit, 100);
        }
    }
}
