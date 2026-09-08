using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.DTOs.Property;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAppointmentHistoryController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var result = new List<PropertyAiCustomerSalesLeadForecastAppointmentHistoryDto>
        {
            new()
            {
                Id = 1,
                CustomerId = 1,
                AppointmentDate = DateTime.UtcNow,
                Status = "Planlandı"
            }
        };

        return Ok(result);
    }
}
