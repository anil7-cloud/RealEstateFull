using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOpportunityController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _opportunityService;

    public PropertyAiCustomerSalesLeadForecastOpportunityController(
        IPropertyAiCustomerSalesLeadForecastService opportunityService)
    {
        _opportunityService = opportunityService;
    }

    [HttpGet("opportunity/{userId}")]
    public async Task<IActionResult> GetOpportunity(int userId)
    {
        var result = await _opportunityService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("opportunity/history/{userId}")]
    public async Task<IActionResult> GetOpportunityHistory(int userId)
    {
        var result = await _opportunityService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
