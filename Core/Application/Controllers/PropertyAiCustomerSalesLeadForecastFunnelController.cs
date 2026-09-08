using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastFunnelController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastFunnelService _funnelService;

    public PropertyAiCustomerSalesLeadForecastFunnelController(
        IPropertyAiCustomerSalesLeadForecastFunnelService funnelService)
    {
        _funnelService = funnelService;
    }

    [HttpGet("funnel/{userId}")]
    public async Task<IActionResult> GetFunnel(int userId)
    {
        var result = await _funnelService.GetFunnelAsync(userId);

        return Ok(result);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var result = await _funnelService.GetHistoryAsync(userId);

        return Ok(result);
    }
}
