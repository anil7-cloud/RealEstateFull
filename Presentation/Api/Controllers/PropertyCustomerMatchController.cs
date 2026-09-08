using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/property-customer-match")]
    public class PropertyCustomerMatchController : ControllerBase
    {
        private readonly IPropertyCustomerMatchService _service;

        public PropertyCustomerMatchController(
            IPropertyCustomerMatchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PropertyCustomerMatchDto>> GetById(
            int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}")]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>>
            GetByLeadId(int leadId)
        {
            var result = await _service.GetByLeadIdAsync(leadId);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}")]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>>
            GetByPropertyId(int propertyId)
        {
            var result = await _service.GetByPropertyIdAsync(propertyId);

            return Ok(result);
        }

        [HttpGet("lead/{leadId:int}/best")]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>>
            GetBestMatchesForLead(
                int leadId,
                [FromQuery] int limit = 10)
        {
            var result = await _service.GetBestMatchesForLeadAsync(
                leadId,
                limit);

            return Ok(result);
        }

        [HttpGet("property/{propertyId:int}/best-leads")]
        public async Task<ActionResult<List<PropertyCustomerMatchDto>>>
            GetBestLeadsForProperty(
                int propertyId,
                [FromQuery] int limit = 10)
        {
            var result = await _service.GetBestLeadsForPropertyAsync(
                propertyId,
                limit);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PropertyCustomerMatchDto>> Create(
            [FromBody] PropertyCustomerMatchDto request)
        {
            if (request.PropertyId <= 0)
                return BadRequest("PropertyId geçerli olmalıdır.");

            if (request.LeadId <= 0)
                return BadRequest("LeadId geçerli olmalıdır.");

            var result = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PropertyCustomerMatchDto>> Update(
            int id,
            [FromBody] PropertyCustomerMatchDto request)
        {
            var result = await _service.UpdateAsync(id, request);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
