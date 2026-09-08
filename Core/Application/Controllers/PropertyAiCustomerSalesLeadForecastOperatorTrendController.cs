using Microsoft.AspNetCore.Mvc;
using REAL_ESTATE_CLEAN.Core.Application.Interfaces.Services;

namespace REAL_ESTATE_CLEAN.Core.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAiCustomerSalesLeadForecastOperatorTrendController : ControllerBase
{
    private readonly IPropertyAiCustomerSalesLeadForecastService _operatorTrendService;

    public PropertyAiCustomerSalesLeadForecastOperatorTrendController(
        IPropertyAiCustomerSalesLeadForecastService operatorTrendService)
    {
        _operatorTrendService = operatorTrendService;
    }

    [HttpGet("operator-trend/{userId}")]
    public async Task<IActionResult> GetOperatorTrend(int userId)
    {
        var result = await _operatorTrendService.GetForecastAsync(userId);

        return Ok(result);
    }

    [HttpGet("operator-trend/history/{userId}")]
    public async Task<IActionResult> GetOperatorTrendHistory(int userId)
    {
        var result = await _operatorTrendService.GetForecastHistoryAsync(userId);

        return Ok(result);
    }
}
