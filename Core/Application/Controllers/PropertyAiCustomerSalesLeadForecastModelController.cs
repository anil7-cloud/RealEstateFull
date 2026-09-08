using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastModelController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastCoreService _modelService;

    public PropertyAiCustomerSalesLeadForecastModelController(
        IPropertyAiCustomerSalesLeadForecastCoreService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet("model/{userId}")]
    public async Task<IActionResult> GetModel(int userId)
    {
        var result = await _modelService.GetMetricsAsync(userId);

        return Ok(result);
    }

    [HttpGet("model/dashboard/{userId}")]
    public async Task<IActionResult> GetModelDashboard(int userId)
    {
        var result = await _modelService.GetDashboardAsync(userId);

        return Ok(result);
    }
}
