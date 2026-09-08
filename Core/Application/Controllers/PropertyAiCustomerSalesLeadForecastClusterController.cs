using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastClusterController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _clusterService;

    public PropertyAiCustomerSalesLeadForecastClusterController(
        IPropertyAiCustomerSalesLeadForecastCoreService clusterService)
    {
        _clusterService = clusterService;
    }

    [HttpGet("cluster/{userId}")]
    public async Task<IActionResult> GetCluster(int userId)
    {
        var result = await _clusterService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("cluster/dashboard/{userId}")]
    public async Task<IActionResult> GetClusterDashboard(int userId)
    {
        var result = await _clusterService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
