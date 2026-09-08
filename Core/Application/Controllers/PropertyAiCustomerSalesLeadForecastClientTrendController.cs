using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastClientTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _clientTrendService;

    public PropertyAiCustomerSalesLeadForecastClientTrendController(
        IPropertyAiCustomerSalesLeadForecastService clientTrendService)
    {
        _clientTrendService = clientTrendService;
    }

    [HttpGet("client-trend/{userId}")]
    public async Task<IActionResult> GetClientTrend(int userId)
    {
        var result = await _clientTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("client-trend/history/{userId}")]
    public async Task<IActionResult> GetClientTrendHistory(int userId)
    {
        var result = await _clientTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
