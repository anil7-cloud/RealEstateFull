using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSegmentTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _segmentTrendService;

    public PropertyAiCustomerSalesLeadForecastSegmentTrendController(
        IPropertyAiCustomerSalesLeadForecastService segmentTrendService)
    {
        _segmentTrendService = segmentTrendService;
    }

    [HttpGet("segment-trend/{userId}")]
    public async Task<IActionResult> GetSegmentTrend(int userId)
    {
        var result = await _segmentTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("segment-trend/history/{userId}")]
    public async Task<IActionResult> GetSegmentTrendHistory(int userId)
    {
        var result = await _segmentTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
