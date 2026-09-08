using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastClientController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _clientService;

    public PropertyAiCustomerSalesLeadForecastClientController(
        IPropertyAiCustomerSalesLeadForecastCoreService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("client/dashboard/{userId}")]
    public async Task<IActionResult> GetClientDashboard(int userId)
    {
        var result = await _clientService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("client/forecast/{userId}")]
    public async Task<IActionResult> GetClientForecast(int userId)
    {
        var result = await _clientService.GetForecastAsync(userId);

        return Ok(result);
    }
}
