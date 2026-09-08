using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSecurityTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _securityTrendService;

    public PropertyAiCustomerSalesLeadForecastSecurityTrendController(
        IPropertyAiCustomerSalesLeadForecastService securityTrendService)
    {
        _securityTrendService = securityTrendService;
    }

    [HttpGet("security-trend/{userId}")]
    public async Task<IActionResult> GetSecurityTrend(int userId)
    {
        var result = await _securityTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("security-trend/history/{userId}")]
    public async Task<IActionResult> GetSecurityTrendHistory(int userId)
    {
        var result = await _securityTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
