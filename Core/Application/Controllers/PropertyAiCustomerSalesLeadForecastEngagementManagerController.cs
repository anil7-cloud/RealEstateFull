using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastEngagementManagerController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastEngagementService _engagementService;

    public PropertyAiCustomerSalesLeadForecastEngagementManagerController(
        IPropertyAiCustomerSalesLeadForecastEngagementService engagementService)
    {
        _engagementService = engagementService;
    }

    [HttpGet("engagement/{userId}")]
    public async Task<IActionResult> GetEngagement(int userId)
    {
        var result = await _engagementService.GetEngagementAsync(userId);

        return Ok(result);
    }

    [HttpGet("engagement/history/{userId}")]
    public async Task<IActionResult> GetEngagementHistory(int userId)
    {
        var result = await _engagementService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
