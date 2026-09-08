using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Services;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match-history")]
    public class PropertyCustomerMatchHistoryController : ControllerBase
    {
        private readonly PropertyCustomerMatchHistoryService _service;

        public PropertyCustomerMatchHistoryController(
            PropertyCustomerMatchHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyCustomerMatchHistory>>>
            GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PropertyCustomerMatchHistory>>
            GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id geçerli olmalıdır.");

            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("match/{matchId:int}")]
        public async Task<ActionResult<List<PropertyCustomerMatchHistory>>>
            GetByMatch(int matchId)
        {
            if (matchId <= 0)
                return BadRequest("MatchId geçerli olmalıdır.");

            var result = await _service.GetByMatchIdAsync(matchId);

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<ActionResult<List<PropertyCustomerMatchHistory>>>
            GetByLead(int leadId)
        {
            if (leadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _service.GetByLeadIdAsync(leadId);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyCustomerMatchHistory>>>
            GetByProperty(int propertyId)
        {
            if (propertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            var result = await _service.GetByPropertyIdAsync(propertyId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyCustomerMatchHistory>>
            Create([FromBody] PropertyCustomerMatchHistoryCreateRequest request)
        {
            if (request.PropertyCustomerMatchId <= 0)
                return BadRequest("PropertyCustomerMatchId geçerli olmalıdır.");

            if (request.PropertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            if (request.LeadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _service.AddAsync(
                request.PropertyCustomerMatchId,
                request.PropertyId,
                request.LeadId,
                request.PreviousScore,
                request.NewScore,
                request.PreviousStatus,
                request.NewStatus,
                request.ChangeType,
                request.Description,
                request.ChangedBy);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id geçerli olmalıdır.");

            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

    public class PropertyCustomerMatchHistoryCreateRequest
    {
        public int PropertyCustomerMatchId { get; set; }

        public int PropertyId { get; set; }

        public int LeadId { get; set; }

        public decimal? PreviousScore { get; set; }

        public decimal NewScore { get; set; }

        public string PreviousStatus { get; set; } = string.Empty;

        public string NewStatus { get; set; } = string.Empty;

        public string ChangeType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ChangedBy { get; set; } = "System";
    }
}
