using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastClusterTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _clusterTrendService;

    public PropertyAiCustomerSalesLeadForecastClusterTrendController(
        IPropertyAiCustomerSalesLeadForecastService clusterTrendService)
    {
        _clusterTrendService = clusterTrendService;
    }

    [HttpGet("cluster-trend/{userId}")]
    public async Task<IActionResult> GetClusterTrend(int userId)
    {
        var result = await _clusterTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("cluster-trend/history/{userId}")]
    public async Task<IActionResult> GetClusterTrendHistory(int userId)
    {
        var result = await _clusterTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
