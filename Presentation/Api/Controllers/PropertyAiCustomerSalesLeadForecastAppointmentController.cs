using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Presentation.Api.Controllers;

[ApiController]
[Route("api/ai/forecast-appointment")]
public class PropertyAiCustomerSalesLeadForecastAppointmentController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastAppointmentService _service;


    public PropertyAiCustomerSalesLeadForecastAppointmentController(
        IPropertyAiCustomerSalesLeadForecastAppointmentService service)
    {
        _service = service;
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAppointment(int userId)
    {
        return Ok(await _service.GetAppointmentAsync(userId));
    }


    [HttpGet("{userId}/history")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        return Ok(await _service.GetHistoryAsync(userId));
    }
}
