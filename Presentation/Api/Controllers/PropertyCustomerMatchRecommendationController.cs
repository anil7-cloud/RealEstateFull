using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Services.Ai;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-recommendation")]
    public class PropertyCustomerMatchRecommendationController : ControllerBase
    {
        private readonly PropertyCustomerMatchRecommendationService
            _recommendationService;

        private readonly PropertyCustomerBulkMatchingService
            _bulkMatchingService;

        public PropertyCustomerMatchRecommendationController(
            PropertyCustomerMatchRecommendationService recommendationService,
            PropertyCustomerBulkMatchingService bulkMatchingService)
        {
            _recommendationService = recommendationService;
            _bulkMatchingService = bulkMatchingService;
        }

        [HttpPost("lead/{leadId:int}/generate")]
        public async Task<ActionResult<BulkLeadMatchResult>>
            GenerateForLead(
                int leadId,
                [FromQuery] int limit = 100)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            try
            {
                var result = await _bulkMatchingService
                    .GenerateForLeadAsync(
                        leadId,
                        limit);

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

        [HttpGet("lead/{leadId:int}")]
        public async Task<ActionResult<List<PropertyMatchRecommendationDto>>>
            GetForLead(
                int leadId,
                [FromQuery] decimal minimumScore = 60,
                [FromQuery] int limit = 10)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _recommendationService
                .GetRecommendationsForLeadAsync(
                    leadId,
                    minimumScore,
                    limit);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyMatchRecommendationDto>>>
            GetForProperty(
                int propertyId,
                [FromQuery] decimal minimumScore = 60,
                [FromQuery] int limit = 10)
        {
            if (propertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            var result = await _recommendationService
                .GetRecommendedLeadsForPropertyAsync(
                    propertyId,
                    minimumScore,
                    limit);

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}/top")]
        public async Task<ActionResult<PropertyMatchRecommendationDto>>
            GetTopForLead(int leadId)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _recommendationService
                .GetRecommendationsForLeadAsync(
                    leadId,
                    0,
                    1);

            var recommendation = result.FirstOrDefault();

            if (recommendation == null)
            {
                return NotFound(new
                {
                    message = "Lead için eşleşme bulunamadı."
                });
            }

            return Ok(recommendation);
        }

        [HttpGet("property/{propertyId:int}/top-lead")]
        public async Task<ActionResult<PropertyMatchRecommendationDto>>
            GetTopLeadForProperty(int propertyId)
        {
            if (propertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            var result = await _recommendationService
                .GetRecommendedLeadsForPropertyAsync(
                    propertyId,
                    0,
                    1);

            var recommendation = result.FirstOrDefault();

            if (recommendation == null)
            {
                return NotFound(new
                {
                    message = "İlan için uygun lead bulunamadı."
                });
            }

            return Ok(recommendation);
        }

        [HttpPost("property/{propertyId:int}/generate")]
        public async Task<ActionResult<BulkPropertyMatchResult>>
            GenerateForProperty(
                int propertyId,
                [FromQuery] int limit = 100)
        {
            if (propertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            try
            {
                var result = await _bulkMatchingService
                    .GenerateForPropertyAsync(
                        propertyId,
                        limit);

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

    }
}
