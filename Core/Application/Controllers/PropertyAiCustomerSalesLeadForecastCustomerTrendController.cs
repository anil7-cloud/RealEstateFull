using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastCustomerTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _customerTrendService;

    public PropertyAiCustomerSalesLeadForecastCustomerTrendController(
        IPropertyAiCustomerSalesLeadForecastService customerTrendService)
    {
        _customerTrendService = customerTrendService;
    }

    [HttpGet("customer-trend/{userId}")]
    public async Task<IActionResult> GetCustomerTrend(int userId)
    {
        var result = await _customerTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("customer-trend/history/{userId}")]
    public async Task<IActionResult> GetCustomerTrendHistory(int userId)
    {
        var result = await _customerTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
