using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-auto-match")]
    public class PropertyCustomerAutoMatchController : ControllerBase
    {
        private readonly PropertyCustomerAutoMatchService _autoMatchService;
        private readonly PropertyCustomerAutomaticMatchingService _automaticMatchingService;

        public PropertyCustomerAutoMatchController(
            PropertyCustomerAutoMatchService autoMatchService,
            PropertyCustomerAutomaticMatchingService automaticMatchingService)
        {
            _autoMatchService = autoMatchService;
            _automaticMatchingService = automaticMatchingService;
        }

        [HttpPost]
        public async Task<ActionResult<PropertyCustomerMatchDto>> CreateMatch(
            [FromBody] AutoMatchRequest request)
        {
            if (request.PropertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            if (request.LeadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var scoreRequest = new PropertyMatchScoreRequest
            {
                PriceMatched = request.PriceMatched,
                LocationMatched = request.LocationMatched,
                PropertyTypeMatched = request.PropertyTypeMatched,
                RoomCountMatched = request.RoomCountMatched,
                SizeMatched = request.SizeMatched
            };

            var result = await _autoMatchService.CreateMatchAsync(
                request.PropertyId,
                request.LeadId,
                scoreRequest);

            return Ok(result);
        }

        [HttpPost("automatic")]
        public async Task<ActionResult<object>> CreateAutomaticMatch(
            [FromBody] AutomaticMatchRequest request)
        {
            if (request.PropertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            if (request.LeadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            try
            {
                var result = await _automaticMatchingService.MatchAsync(
                    request.PropertyId,
                    request.LeadId);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }

        [HttpGet("lead/{leadId:int}/recommendations")]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>>
            GetLeadRecommendations(
                int leadId,
                [FromQuery] int limit = 10)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _autoMatchService
                .GetLeadRecommendationsAsync(
                    leadId,
                    limit);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}/leads")]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>>
            GetPropertyLeadRecommendations(
                int propertyId,
                [FromQuery] int limit = 10)
        {
            if (propertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            var result = await _autoMatchService
                .GetPropertyLeadRecommendationsAsync(
                    propertyId,
                    limit);

            return Ok(result);
        }
    }

    public class AutoMatchRequest
    {
        public int PropertyId { get; set; }
        public int LeadId { get; set; }
        public bool PriceMatched { get; set; }
        public bool LocationMatched { get; set; }
        public bool PropertyTypeMatched { get; set; }
        public bool RoomCountMatched { get; set; }
        public bool SizeMatched { get; set; }
    }

    public class AutomaticMatchRequest
    {
        public int PropertyId { get; set; }
        public int LeadId { get; set; }
    }
}
