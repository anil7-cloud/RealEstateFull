using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastTrainingTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _trainingTrendService;

    public PropertyAiCustomerSalesLeadForecastTrainingTrendController(
        IPropertyAiCustomerSalesLeadForecastService trainingTrendService)
    {
        _trainingTrendService = trainingTrendService;
    }

    [HttpGet("training-trend/{userId}")]
    public async Task<IActionResult> GetTrainingTrend(int userId)
    {
        var result = await _trainingTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("training-trend/history/{userId}")]
    public async Task<IActionResult> GetTrainingTrendHistory(int userId)
    {
        var result = await _trainingTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
