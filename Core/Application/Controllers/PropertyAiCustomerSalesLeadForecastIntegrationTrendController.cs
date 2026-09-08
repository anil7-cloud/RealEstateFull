using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastIntegrationTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _integrationTrendService;

    public PropertyAiCustomerSalesLeadForecastIntegrationTrendController(
        IPropertyAiCustomerSalesLeadForecastService integrationTrendService)
    {
        _integrationTrendService = integrationTrendService;
    }

    [HttpGet("integration-trend/{userId}")]
    public async Task<IActionResult> GetIntegrationTrend(int userId)
    {
        var result = await _integrationTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("integration-trend/history/{userId}")]
    public async Task<IActionResult> GetIntegrationTrendHistory(int userId)
    {
        var result = await _integrationTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
