using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPortalTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _portalTrendService;

    public PropertyAiCustomerSalesLeadForecastPortalTrendController(
        IPropertyAiCustomerSalesLeadForecastService portalTrendService)
    {
        _portalTrendService = portalTrendService;
    }

    [HttpGet("portal-trend/{userId}")]
    public async Task<IActionResult> GetPortalTrend(int userId)
    {
        var result = await _portalTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("portal-trend/history/{userId}")]
    public async Task<IActionResult> GetPortalTrendHistory(int userId)
    {
        var result = await _portalTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
