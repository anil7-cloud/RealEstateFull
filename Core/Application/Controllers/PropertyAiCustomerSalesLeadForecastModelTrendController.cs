using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastModelTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _modelTrendService;

    public PropertyAiCustomerSalesLeadForecastModelTrendController(
        IPropertyAiCustomerSalesLeadForecastService modelTrendService)
    {
        _modelTrendService = modelTrendService;
    }

    [HttpGet("model-trend/{userId}")]
    public async Task<IActionResult> GetModelTrend(int userId)
    {
        var result = await _modelTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("model-trend/history/{userId}")]
    public async Task<IActionResult> GetModelTrendHistory(int userId)
    {
        var result = await _modelTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
