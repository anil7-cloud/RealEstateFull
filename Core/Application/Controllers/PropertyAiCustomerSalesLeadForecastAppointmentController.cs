using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastAppointmentController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastAppointmentService _appointmentService;

    public PropertyAiCustomerSalesLeadForecastAppointmentController(
        IPropertyAiCustomerSalesLeadForecastAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("appointment/{userId}")]
    public async Task<IActionResult> GetAppointment(int userId)
    {
        var result = await _appointmentService.GetAppointmentAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _appointmentService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
