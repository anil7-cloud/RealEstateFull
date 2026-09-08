using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPriorityTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _priorityTrendService;

    public PropertyAiCustomerSalesLeadForecastPriorityTrendController(
        IPropertyAiCustomerSalesLeadForecastService priorityTrendService)
    {
        _priorityTrendService = priorityTrendService;
    }

    [HttpGet("priority-trend/{userId}")]
    public async Task<IActionResult> GetPriorityTrend(int userId)
    {
        var result = await _priorityTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("priority-trend/history/{userId}")]
    public async Task<IActionResult> GetPriorityTrendHistory(int userId)
    {
        var result = await _priorityTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
