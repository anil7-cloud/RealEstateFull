using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPriorityController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _priorityService;

    public PropertyAiCustomerSalesLeadForecastPriorityController(
        IPropertyAiCustomerSalesLeadForecastCoreService priorityService)
    {
        _priorityService = priorityService;
    }

    [HttpGet("priority/{userId}")]
    public async Task<IActionResult> GetPriority(int userId)
    {
        var result = await _priorityService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("priority/dashboard/{userId}")]
    public async Task<IActionResult> GetPriorityDashboard(int userId)
    {
        var result = await _priorityService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
