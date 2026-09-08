using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastQuotaController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _quotaService;

    public PropertyAiCustomerSalesLeadForecastQuotaController(
        IPropertyAiCustomerSalesLeadForecastCoreService quotaService)
    {
        _quotaService = quotaService;
    }

    [HttpGet("quota/{userId}")]
    public async Task<IActionResult> GetQuota(int userId)
    {
        var result = await _quotaService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("quota/dashboard/{userId}")]
    public async Task<IActionResult> GetQuotaDashboard(int userId)
    {
        var result = await _quotaService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
