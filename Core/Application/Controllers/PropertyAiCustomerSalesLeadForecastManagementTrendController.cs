using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastManagementTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _managementTrendService;

    public PropertyAiCustomerSalesLeadForecastManagementTrendController(
        IPropertyAiCustomerSalesLeadForecastService managementTrendService)
    {
        _managementTrendService = managementTrendService;
    }

    [HttpGet("management-trend/{userId}")]
    public async Task<IActionResult> GetManagementTrend(int userId)
    {
        var result = await _managementTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("management-trend/history/{userId}")]
    public async Task<IActionResult> GetManagementTrendHistory(int userId)
    {
        var result = await _managementTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
