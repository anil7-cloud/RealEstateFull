using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastPartnerTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _partnerTrendService;

    public PropertyAiCustomerSalesLeadForecastPartnerTrendController(
        IPropertyAiCustomerSalesLeadForecastService partnerTrendService)
    {
        _partnerTrendService = partnerTrendService;
    }

    [HttpGet("partner-trend/{userId}")]
    public async Task<IActionResult> GetPartnerTrend(int userId)
    {
        var result = await _partnerTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("partner-trend/history/{userId}")]
    public async Task<IActionResult> GetPartnerTrendHistory(int userId)
    {
        var result = await _partnerTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
