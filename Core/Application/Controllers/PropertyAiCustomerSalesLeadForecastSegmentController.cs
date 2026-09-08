using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastSegmentController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _segmentService;

    public PropertyAiCustomerSalesLeadForecastSegmentController(
        IPropertyAiCustomerSalesLeadForecastCoreService segmentService)
    {
        _segmentService = segmentService;
    }

    [HttpGet("segment/{userId}")]
    public async Task<IActionResult> GetSegment(int userId)
    {
        var result = await _segmentService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("segment/dashboard/{userId}")]
    public async Task<IActionResult> GetSegmentDashboard(int userId)
    {
        var result = await _segmentService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
