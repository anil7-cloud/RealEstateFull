using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-sales-risk")]
    public class PropertyCustomerMatchSalesRiskController
        : ControllerBase
    {
        private readonly PropertyCustomerMatchSalesRiskService _service;

        public PropertyCustomerMatchSalesRiskController(
            PropertyCustomerMatchSalesRiskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
            GetAll([FromQuery] int limit = 50)
        {
            var result = await _service.GetRisksAsync(
                NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<ActionResult<PropertyMatchSalesRiskDto>>
            GetForMatch(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest(new
                {
                    message = "MatchId geçerli olmalıdır."
                });
            }

            var result = await _service
                .GetMatchRiskAsync(matchId);

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
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
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

            var result = await _service.GetLeadRisksAsync(
                leadId,
                NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
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

            var result = await _service.GetPropertyRisksAsync(
                propertyId,
                NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<PropertyMatchSalesRiskSummaryDto>>
            GetSummary()
        {
            var result = await _service.GetSummaryAsync();

            return Ok(result);
        }

        [HttpGet("critical")]
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
            GetCritical([FromQuery] int limit = 20)
        {
            var risks = await _service.GetRisksAsync(100);

            var result = risks
                .Where(x =>
                    string.Equals(
                        x.RiskLevel,
                        "Critical",
                        StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.RiskScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("high")]
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
            GetHighRisk([FromQuery] int limit = 30)
        {
            var risks = await _service.GetRisksAsync(100);

            var result = risks
                .Where(x =>
                    x.RiskLevel == "Critical" ||
                    x.RiskLevel == "VeryHigh" ||
                    x.RiskLevel == "High")
                .OrderByDescending(x => x.RiskScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("immediate-action")]
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
            GetImmediateAction(
                [FromQuery] int limit = 20)
        {
            var risks = await _service.GetRisksAsync(100);

            var result = risks
                .Where(x => x.RequiresImmediateAction)
                .OrderByDescending(x => x.RiskScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("low-match")]
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
            GetLowMatch(
                [FromQuery] decimal maximumMatchScore = 50,
                [FromQuery] int limit = 30)
        {
            maximumMatchScore = Math.Clamp(
                maximumMatchScore,
                0,
                100);

            var risks = await _service.GetRisksAsync(100);

            var result = risks
                .Where(x =>
                    x.MatchScore <= maximumMatchScore)
                .OrderByDescending(x => x.RiskScore)
                .Take(NormalizeLimit(limit))
                .ToList();

            return Ok(result);
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<PropertyMatchSalesRiskDto>>>
            GetTop([FromQuery] int limit = 10)
        {
            var result = await _service.GetRisksAsync(
                NormalizeLimit(limit));

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var summaryTask =
                _service.GetSummaryAsync();

            var risksTask =
                _service.GetRisksAsync(100);

            await Task.WhenAll(
                summaryTask,
                risksTask);

            var summary = await summaryTask;
            var risks = await risksTask;

            var immediate = risks
                .Where(x => x.RequiresImmediateAction)
                .OrderByDescending(x => x.RiskScore)
                .Take(10)
                .ToList();

            var topRisks = risks
                .OrderByDescending(x => x.RiskScore)
                .Take(10)
                .ToList();

            return Ok(new
            {
                summary,

                immediateActions = immediate,

                topRisks,

                generatedAt = DateTime.UtcNow
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                service =
                    "PropertyCustomerMatchSalesRisk",

                status = "Running",

                timestamp = DateTime.UtcNow
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
