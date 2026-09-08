using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOperatorController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _operatorService;

    public PropertyAiCustomerSalesLeadForecastOperatorController(
        IPropertyAiCustomerSalesLeadForecastCoreService operatorService)
    {
        _operatorService = operatorService;
    }

    [HttpGet("operator/dashboard/{userId}")]
    public async Task<IActionResult> GetOperatorDashboard(int userId)
    {
        var result = await _operatorService.GetDashboardAsync(userId);

        return Ok(result);
    }

    [HttpGet("operator/forecast/{userId}")]
    public async Task<IActionResult> GetOperatorForecast(int userId)
    {
        var result = await _operatorService.GetForecastAsync(userId);

        return Ok(result);
    }
}
