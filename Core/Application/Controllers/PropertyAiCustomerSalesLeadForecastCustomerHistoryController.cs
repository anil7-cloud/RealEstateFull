using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastCustomerHistoryController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _customerService;

    public PropertyAiCustomerSalesLeadForecastCustomerHistoryController(
        IPropertyAiCustomerSalesLeadForecastService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("customer-history/{userId}")]
    public async Task<IActionResult> GetCustomerHistory(int userId)
    {
        var result = await _customerService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }

    [HttpGet("customer/{userId}")]
    public async Task<IActionResult> GetCustomerForecast(int userId)
    {
        var result = await _customerService.GetForecastAsync(userId);

        return Ok(result);
    }
}
